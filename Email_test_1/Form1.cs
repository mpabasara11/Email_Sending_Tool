using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
namespace Email_test_1
{
    public partial class Form1 : Form
    {
        NetworkCredential login;
        SmtpClient client;
        MailMessage msg;
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_send_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    MailMessage mail = new MailMessage("antolaka3@gmail.com", txt_to.Text,txt_sub.Text,txt_msg.Text);
            //    SmtpClient client = new SmtpClient("smtp.gmail.com");
            //    client.Port = 587;
            //    client.Credentials= new NetworkCredential("antolaka3@gmail.com","Lakaanto1997");
            //    client.EnableSsl = true;
            //    client.Send(mail);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            login = new NetworkCredential(txt_username.Text, txt_password.Text);
            client = new SmtpClient(txt_smtp.Text);
            client.Port = Convert.ToInt32(txt_port.Text);
            client.EnableSsl = chkSSL.Checked;
            client.Credentials = login;
            msg = new MailMessage { From = new MailAddress(txt_username.Text + txt_smtp.Text.Replace("stmp.", "@"), "Laka Anto", Encoding.UTF8) };
            msg.To.Add(new MailAddress(txt_to.Text));
            if (!string.IsNullOrEmpty(txt_cc.Text))
                msg.To.Add(new MailAddress(txt_cc.Text));

            msg.Subject = txt_sub.Text;
            msg.Body = txt_msg.Text;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallBack);
            string userstate = "Sending.......";
            client.SendAsync(msg, userstate);
        }

        private void SendCompletedCallBack(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show(string.Format("{0} send canceled", e.UserState), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (e.Error != null)
                MessageBox.Show(string.Format("{0} {1} send canceled", e.UserState, e.Error), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Your Message has Been successfully Sent. ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
