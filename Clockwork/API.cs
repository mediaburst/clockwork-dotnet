using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace Clockwork
{
    /// <summary>
    /// .NET Wrapper for the Clockwork SMS API
    /// </summary>
    public class API
    {
        private const string smsUrl = "api.clockworksms.com/xml/send";
        private const string creditUrl = "api.clockworksms.com/xml/credit";
        private const string balanceUrl = "api.clockworksms.com/xml/balance";

        /// <summary>
        /// Clockwork API Key
        /// </summary>
        /// <remarks>
        /// Log in to the Clockwork website to generate a new key for your account
        /// </remarks>
        public string Key { get; set; }

        /// <summary>
        /// Use SSL when making requests
        /// </summary>
        public bool SSL { get; set; }

        /// <summary>
        /// A System.Net.WebProxy object containing your proxy server details.
        /// Only set this if you need to override your servers default proxy settings
        /// If you don't use a proxy server just leave it as null
        /// </summary>
        public WebProxy Proxy { get; set; }

        /// <summary>
        /// (Optional) From address displayed on the user's phone
        /// </summary>
        /// <remarks>
        /// If left blank your account default will be used
        /// </remarks>
        public string From { get; set; }

        /// <summary>
        /// (Optional) Send message longer than 160 characters
        /// </summary>
        /// <remarks>
        /// If left blank your account default will be used
        /// </remarks>        
        public bool? Long { get; set; }

        /// <summary>
        /// (Optional) Trim the message text if it's too long
        /// </summary>
        /// <remarks>
        /// If left blank your account default will be used
        /// </remarks>        
        public bool? Truncate { get; set; }

        /// <summary>
        /// What to do if there's an invalid character in your message text
        /// Valid characters are defined in the GSM 03.38 character set
        /// </summary>
        /// /// <remarks>
        /// Set this to AccountDefault to use your accounts default setting
        /// </remarks>        
        public InvalidCharacterAction InvalidCharacterAction { get; set; }        

        /// <summary>
        /// Create a new API wrapper
        /// </summary>
        /// <param name="key">Clockwork API Key</param>
        public API(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentException("API Key can't be blank", "key");

            Key = key;

            // Set some sensible & secure defaults
            SSL = true;
            Proxy = null;

            // SMS defaults - Account defaults will be used if these are left as null
            From = null;
            Long = null;
            Truncate = null;
            InvalidCharacterAction = global::InvalidCharacterAction.AccountDefault;
        }

        /// <summary>
        /// Send a single text message
        /// </summary>
        /// <param name="sms">A single SMS</param>
        /// <returns>An SMSResult object</returns>
        public SMSResult Send(SMS sms)
        {
            if (sms == null)
                throw new ArgumentException("SMS can't be empty", "sms");

            List<SMS> list = new List<SMS>();
            list.Add(sms);
            return this.Send(list)[0];
        }

        /// <summary>
        /// Send a list of text messages
        /// </summary>
        /// <param name="list">List of SMS messages</param>
        /// <returns>List of SMSResult objects, one per SMS</returns>
        public List<SMSResult> Send(List<SMS> smsList)
        {
            if (smsList == null || smsList.Count == 0)
                throw new ArgumentException("SMS list can't be empty", "smsList");

            int wrapperId = 0;
            XML.Message xmlMessage = new XML.Message { Key = Key };
            foreach (SMS sms in smsList)
            {
                xmlMessage.SMS.Add(new XML.SMS 
                { 
                    To                  = sms.To, 
                    Content             = sms.Message,
                    From                = (String.IsNullOrEmpty(sms.From)) ? From : sms.From,
                    ClientID            = sms.ClientID,
                    Long                = (sms.Long == null) ? Long : sms.Long,
                    Truncate            = (sms.Truncate == null) ? Truncate : sms.Truncate,
                    InvalidCharAction   = (sms.InvalidCharacterAction == null) ? InvalidCharacterAction : sms.InvalidCharacterAction,
                    WrapperID           = wrapperId++
                });                
            }

            XML.Message_Resp xmlResp = new XML.Message_Resp();
            XmlPost<XML.Message, XML.Message_Resp>(smsUrl, ref xmlMessage, ref xmlResp);

            // Check for general error
            if ((xmlResp.ErrNo ?? 0) > 0)
                throw new APIException(xmlResp.ErrDesc, xmlResp.ErrNo ?? 0);

            List<SMSResult> results = new List<SMSResult>();
            foreach (XML.SMS_Resp resp in xmlResp.SMS_Resp)
            {
                results.Add(new SMSResult
                {
                    SMS             = smsList[resp.WrapperID],
                    ID              = resp.MessageID,
                    ErrorCode       = resp.ErrNo ?? 0,
                    ErrorMessage    = resp.ErrDesc,
                    Success         = ((resp.ErrNo ?? 0) == 0)
                });
            }

            return results;
        }
        
        /// <summary>
        /// Check how many SMS credits are available on your account
        /// </summary>
        /// <returns>Number of SMS</returns>
        [Obsolete("Use the GetBalance function that returns cash balance instead", false)]
        public long CheckCredit()
        {
            XML.Credit xmlReq = new XML.Credit { Key = Key };
            XML.Credit_Resp xmlResp = new XML.Credit_Resp();
            XmlPost<XML.Credit, XML.Credit_Resp>(creditUrl, ref xmlReq, ref xmlResp);

            // Check for general error
            if ((xmlResp.ErrNo ?? 0) > 0)
                throw new APIException(xmlResp.ErrDesc, xmlResp.ErrNo ?? 0);

            return xmlResp.Credit;
        }

        /// <summary>
        /// Check your current Clockwork balance
        /// </summary>
        /// <returns>Balance object containing current balance and currency information</returns>
        public Balance GetBalance()
        {
            XML.Balance xmlReq = new XML.Balance { Key = Key };
            XML.Balance_Resp xmlResp = new XML.Balance_Resp();
            XmlPost<XML.Balance, XML.Balance_Resp>(balanceUrl, ref xmlReq, ref xmlResp);

            // Check for general error
            if ((xmlResp.ErrNo ?? 0) > 0)
                throw new APIException(xmlResp.ErrDesc, xmlResp.ErrNo ?? 0);

            Balance balance = new Balance
            {
                AccountType = xmlResp.AccountType.Equals("payg", StringComparison.InvariantCultureIgnoreCase) ? AccountType.PayAsYouGo : AccountType.Invoice,
                Amount = xmlResp.Balance,
                CurrencyCode = xmlResp.Currency.Code,
                CurrencySymbol = xmlResp.Currency.Symbol
            };
            return balance;
        }

        // Serialise objects to XML and POST to the Clockwork API
        private void XmlPost<T, U>(string url, ref T send, ref U response)
        {
            if (String.IsNullOrEmpty(url.Trim()))
                throw new ArgumentException("URL Needed for POST", "url");

            if (SSL)
                url = "https://" + url;
            else
                url = "http://" + url;

            UTF8StringWriter outStream = new UTF8StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(outStream, send);
            string req = outStream.ToString();
            outStream.Dispose();

            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(url);
            httpReq.UserAgent = "Clockwork .NET Wrapper/" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            httpReq.Timeout = 60000;
            httpReq.ReadWriteTimeout = 60000;
            httpReq.Method = "POST";
            httpReq.ContentLength = System.Text.Encoding.UTF8.GetByteCount(req);
            httpReq.ContentType = "text/xml; charset=utf-8";
            if (Proxy != null)
                httpReq.Proxy = Proxy;

            StreamWriter myStream = new StreamWriter(httpReq.GetRequestStream());
            myStream.Write(req);
            myStream.Close();

            HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
            if (((HttpWebResponse)httpResp).StatusCode == HttpStatusCode.OK)
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(U));
                response = (U)deserializer.Deserialize(httpResp.GetResponseStream());
            }
            httpResp.Close();
        }
    }
}
