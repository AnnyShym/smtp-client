using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace SimpleSmtpClient
{
    public partial class mainForm : Form
    {

        public mainForm()
        {
            InitializeComponent();
        }

        private void guiUseCredentials_CheckedChanged(object sender, EventArgs e)
        {
            if (guiUseCredentials.Checked)
            {
                guiUser.ReadOnly = false;
                guiPassword.ReadOnly = false;
            }
            else
            {
                guiUser.ReadOnly = true;
                guiPassword.ReadOnly = true;
            }
        }

        private void guiSendMail_Click(object sender, EventArgs e)
        {
            try
            {

                var fromAddress = new MailAddress(guiEmailFrom.Text, guiEmailFrom.Text);
                var toAddress = new MailAddress(guiEmailTo.Text, guiEmailTo.Text);

                SmtpClient client = new SmtpClient();
                client.Host = guiServerName.Text;
                client.Port = Convert.ToInt32(guiPort.Text);
                client.EnableSsl = true;

                if (guiUseCredentials.Checked)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(fromAddress.Address, guiPassword.Text);
                }

                client.Timeout = 20000;

                MailMessage message = new MailMessage(fromAddress, toAddress);
                message.Subject = guiEmailSubject.Text;
                message.Body = guiEmailBody.Text;

                client.Send(message);

                MessageBox.Show("Email Sent.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

     }
}
