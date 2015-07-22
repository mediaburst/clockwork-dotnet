using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
#if PCL
using System.Net.Http;
using System.Net.Http.Headers;
#endif
using System.Xml.Serialization;

namespace Clockwork
{
    /// <summary>
    /// .NET Wrapper for the Clockwork SMS API
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class API
    {
        private const string BaseUrl = "api.clockworksms.com/";
        private const string SmsUrl = "xml/send";
        private const string CreditUrl = "xml/credit";
        private const string BalanceUrl = "xml/balance";

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
        // ReSharper disable once InconsistentNaming
        public bool SSL { get; set; }

        /// <summary>
        /// A System.Net.WebProxy object containing your proxy server details.
        /// Only set this if you need to override your servers default proxy settings
        /// If you don't use a proxy server just leave it as null
        /// </summary>
        public IWebProxy Proxy { get; set; }

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
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("API Key can't be blank", nameof(key));

            Key = key;

            // Set some sensible & secure defaults
            SSL = true;
            
            // SMS defaults - Account defaults will be used if these are left as null
            From = null;
            Long = null;
            Truncate = null;
            InvalidCharacterAction = InvalidCharacterAction.AccountDefault;
        }

        /// <summary>
        /// Send a single text message
        /// </summary>
        /// <param name="sms">A single SMS</param>
        /// <returns>An SMSResult object</returns>
        public SMSResult Send(SMS sms)
        {
            if (sms == null)
                throw new ArgumentException("SMS can't be empty", nameof(sms));

            var list = new List<SMS> {sms};
            return Send(list)[0];
        }

        /// <summary>
        /// Send a list of text messages
        /// </summary>
        /// <param name="smsList">List of SMS messages</param>
        /// <returns>List of SMSResult objects, one per SMS</returns>
        public List<SMSResult> Send(List<SMS> smsList)
        {
            if (smsList == null || smsList.Count == 0)
                throw new ArgumentException("SMS list can't be empty", nameof(smsList));

            var wrapperId = 0;
            var xmlMessage = new XML.Message { Key = Key };
            foreach (var sms in smsList)
            {
                xmlMessage.SMS.Add(new XML.SMS 
                { 
                    To                  = sms.To, 
                    Content             = sms.Message,
                    From                = (string.IsNullOrEmpty(sms.From)) ? From : sms.From,
                    ClientID            = sms.ClientID,
                    Long                = sms.Long ?? Long,
                    Truncate            = sms.Truncate ?? Truncate,
                    InvalidCharAction   = sms.InvalidCharacterAction ?? InvalidCharacterAction,
                    WrapperID           = wrapperId++
                });                
            }

            var xmlResp = new XML.Message_Resp();
            XmlPost(SmsUrl, ref xmlMessage, ref xmlResp);

            // Check for general error
            if ((xmlResp.ErrNo ?? 0) > 0)
                throw new APIException(xmlResp.ErrDesc, xmlResp.ErrNo ?? 0);

            var results = new List<SMSResult>();
            foreach (var resp in xmlResp.SMS_Resp)
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
            var xmlReq = new XML.Credit { Key = Key };
            var xmlResp = new XML.Credit_Resp();
            XmlPost(CreditUrl, ref xmlReq, ref xmlResp);

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
            var xmlReq = new XML.Balance { Key = Key };
            var xmlResp = new XML.Balance_Resp();
            XmlPost(BalanceUrl, ref xmlReq, ref xmlResp);

            // Check for general error
            if ((xmlResp.ErrNo ?? 0) > 0)
                throw new APIException(xmlResp.ErrDesc, xmlResp.ErrNo ?? 0);

            var balance = new Balance
            {
                AccountType = xmlResp.AccountType.Equals("payg", StringComparison.OrdinalIgnoreCase) ? AccountType.PayAsYouGo : AccountType.Invoice,
                Amount = xmlResp.Balance,
                CurrencyCode = xmlResp.Currency.Code,
                CurrencySymbol = xmlResp.Currency.Symbol
            };
            return balance;
        }

        // Serialise objects to XML and POST to the Clockwork API
        private void XmlPost<TS, TR>(string url, ref TS send, ref TR response)
        {
            if (string.IsNullOrEmpty(url.Trim()))
                throw new ArgumentException("URL Needed for POST", nameof(url));

            var outStream = new UTF8StringWriter();
            var serializer = new XmlSerializer(typeof(TS));
            serializer.Serialize(outStream, send);
            var req = outStream.ToString();
            outStream.Dispose();


#if PCL
            var clientHandler = new HttpClientHandler
            {
                Proxy = Proxy,
                UseProxy = Proxy != null,
                UseCookies = false
            };
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri((SSL ? "https://" : "http://") + BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                var result = client.PostAsync(SmsUrl, new StringContent(req, Encoding.UTF8)).Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseStream = result.Content.ReadAsStreamAsync().Result;

                    var deserializer = new XmlSerializer(typeof(TR));
                    if (responseStream == null)
                        throw new WebException("Blank response");
                    response = (TR)deserializer.Deserialize(responseStream);
                }

            }
            
#else
            if (SSL)
                url = "https://" + BaseUrl + url;
            else
                url = "http://" + BaseUrl + url;


            var httpReq = (HttpWebRequest)WebRequest.Create(url);
            httpReq.UserAgent = "Clockwork .NET Wrapper/" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            httpReq.Timeout = 60000;
            httpReq.ReadWriteTimeout = 60000;
            httpReq.Method = "POST";
            httpReq.ContentLength = System.Text.Encoding.UTF8.GetByteCount(req);
            httpReq.ContentType = "text/xml; charset=utf-8";
            if (Proxy != null)
                httpReq.Proxy = Proxy;

            var myStream = new StreamWriter(httpReq.GetRequestStream());
            myStream.Write(req);
            myStream.Close();

            var httpResp = (HttpWebResponse)httpReq.GetResponse();
            if (httpResp.StatusCode == HttpStatusCode.OK)
            {
                var deserializer = new XmlSerializer(typeof(TR));
                var responseStream = httpResp.GetResponseStream();
                if(responseStream == null)
                    throw new WebException("Blank response");
                response = (TR)deserializer.Deserialize(responseStream);
            }
            httpResp.Close();
#endif

        }
    }
}
