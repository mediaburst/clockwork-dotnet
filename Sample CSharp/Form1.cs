using System;
using System.Net;
using System.Windows.Forms;
using Clockwork;

namespace Clockwork.Samples.CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                API api = new API(key.Text);

                SMS sms = new SMS
                {
                    To = to.Text,
                    Message = message.Text
                };

                SMSResult result = api.Send(sms);

                if (result.Success)
                    MessageBox.Show("Sent\nID: " + result.ID);
                else
                    MessageBox.Show("Error: " + result.ErrorMessage);
            }
            catch (APIException ex)
            {
                // You'll get an API exception for errors 
                // such as wrong key
                MessageBox.Show("API Exception: " + ex.Message);
            }
            catch (WebException ex)
            {
                // Web exceptions mean you couldn't reach the mediaburst server
                MessageBox.Show("Web Exception: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                // Argument exceptions are thrown for missing parameters,
                // such as forgetting to set the username
                MessageBox.Show("Argument Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Something else went wrong, the error message should help
                MessageBox.Show("Unknown Exception: " + ex.Message);
            }
        }
    }
}
