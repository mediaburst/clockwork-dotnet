using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clockwork.Tests
{
    /// <summary>
    /// Test the Clockwork wrapper
    /// </summary>
    [TestClass]
    public class APITest
    {
        private static string key;

        public APITest()
        {
            
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) 
        {
            key = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            if (String.IsNullOrWhiteSpace(key))
                throw new ApplicationException("You need to set an API Key in app.config before running these tests");
        }
        
        [TestMethod]
        public void Constructor()
        {
            Clockwork.API api = new API(key);
            Assert.IsInstanceOfType(api, typeof(API));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Null key constructor worked")]
        public void ConstructorNullKey()
        {
            Clockwork.API api = new API(null);
            Assert.IsInstanceOfType(api, typeof(API));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Blank key constructor worked")]
        public void ConstructorBlankKey()
        {
            Clockwork.API api = new API("");
            Assert.IsInstanceOfType(api, typeof(API));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Null sms seend worked")]
        public void SendNullSMS_Single()
        {
            Clockwork.API api = new API(key);
            SMS sms = null;
            SMSResult result = api.Send(sms);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Null sms list send worked")]
        public void SendNullSMS_List()
        {
            Clockwork.API api = new API(key);
            List<SMS> smsList = null;
            List<SMSResult> result = api.Send(smsList);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Empty sms list send worked")]
        public void SendEmptySMS_List()
        {
            List<SMS> sms = new List<SMS>();
            Clockwork.API api = new API(key);
            List<SMSResult> result = api.Send(sms);
        }

        [TestMethod]
        public void SendOneSMS_Single()
        {
            Clockwork.API api = new API(key);
            SMSResult result = api.Send(new SMS { To = "441234567890", Message = "Hello World" });
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void SendOneSMS_List()
        {
            Clockwork.API api = new API(key);

            List<SMS> sms = new List<SMS>();
            sms.Add(new SMS { To = "441234567891", Message = "Hello World" });
            
            List<SMSResult> result = api.Send(sms);
            Assert.IsTrue(result[0].Success);
        }

        [TestMethod]
        public void SendMultiSMS_Success()
        {
            Clockwork.API api = new API(key);

            List<SMS> smsList = new List<SMS>();
            smsList.Add(new SMS { To = "441234567891", Message = "Hello World" });
            smsList.Add(new SMS { To = "441234567892", Message = "Hello World" });

            List<SMSResult> result = api.Send(smsList);
            Assert.IsTrue(result[0].Success && result[1].Success);
        }

        [TestMethod]
        public void SendMultiSMS_BothNoMessage()
        {
            Clockwork.API api = new API(key);

            List<SMS> smsList = new List<SMS>();
            smsList.Add(new SMS { To = "441234567891" });
            smsList.Add(new SMS { To = "441234567892" });

            List<SMSResult> result = api.Send(smsList);
            Assert.IsFalse(result[0].Success || result[1].Success);
        }

        [TestMethod]
        public void SendMultiSMS_OneNoMessage()
        {
            Clockwork.API api = new API(key);

            List<SMS> smsList = new List<SMS>();
            smsList.Add(new SMS { To = "441234567891", Message="Hello World" });
            smsList.Add(new SMS { To = "441234567892" });

            List<SMSResult> result = api.Send(smsList);
            Assert.IsTrue(result[0].Success);
            Assert.IsFalse(result[1].Success);
        }

        // Check we handle API errors correctly
        [TestMethod()]
        public void SendOneSMS_InvalidNumber()
        {
            Clockwork.API api = new API(key);
            SMSResult result = api.Send(new SMS { To = "not_a_number", Message = "Hello World" });
            
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(10, result.ErrorCode);
        }

        [TestMethod()]
        public void SendOneSMS_UseProxy()
        {
            string proxyHost = "";
            int proxyPort = 0;

            proxyHost = System.Configuration.ConfigurationManager.AppSettings["ProxyHost"];
            if (String.IsNullOrWhiteSpace(proxyHost))
                Assert.Inconclusive("Proxy host not configured - Skipping test");

            string proxyPortTemp = System.Configuration.ConfigurationManager.AppSettings["ProxyPort"];
            if (String.IsNullOrWhiteSpace(proxyPortTemp))
                Assert.Inconclusive("Proxy port not configured - Skipping test");

            if(!int.TryParse(proxyPortTemp, out proxyPort))
                Assert.Inconclusive("Proxy port could not be parsed - Skipping test");
            
            Clockwork.API api = new API(key);

            api.Proxy = new System.Net.WebProxy(proxyHost, proxyPort);

            SMSResult result = api.Send(new SMS { To = "44123457890", Message = "Hello World" });

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void SendOneSMS_Response_Matches_Send()
        {
            Clockwork.API api = new API(key);

            List<SMS> smsList = new List<SMS>();
            SMS sms = new SMS { To = "441234567891", Message = "Hello World" };
            smsList.Add(sms);

            List<SMSResult> result = api.Send(smsList);
            Assert.AreSame(sms, result[0].SMS);
        }

        [TestMethod]
        public void CheckCredit()
        {
            Clockwork.API api = new API(key);
            long credit = api.CheckCredit();
            Assert.IsTrue(credit >= 0);
        }
    }
}
