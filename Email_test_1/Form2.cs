using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Email_test_1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential()
                    {
                        UserName = "lakshananthony13@gmail.com",
                        Password = "yucbknpvcgtnupis"
                    }
                };
                String htmlbody = "<h1>" + txt_title.Text + "</h1>" + "<p>" + txt_message.Text + "</p>" ;
                MailAddress fromEmail = new MailAddress("lakshananthony13@gmail.com", "Antony");
                MailAddress toEmail = new MailAddress(txt_email.Text, "Client");
                MailMessage message = new MailMessage()
                {

                    From = fromEmail,
                    Subject = txt_subject.Text,
                    Body = htmlbody

            };
                message.IsBodyHtml = true;
                message.To.Add(toEmail);
                client.SendCompleted += Client_SendCompleted;
                client.SendMailAsync(message);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Email not send",ex.Message);
            }
        }
        private void Client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Some Error. Please Check Again \n" + e.Error.Message, "Error");
                return;
            }
            MessageBox.Show("Sent Successfully","Done");
        }
    }
}
