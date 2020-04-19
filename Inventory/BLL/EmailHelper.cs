using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Inventory.BLL
{
    public class EmailHelper
    {

        public static void SendEmail(string ToEmail, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailAddress from = new MailAddress(ConfigurationManager.AppSettings["EmailSender"], ConfigurationManager.AppSettings["EmailSenderName"]);
            MailAddress to = new MailAddress(ToEmail, ToEmail);
            MailMessage mail = new MailMessage(from, to);
            mail.Subject = subject;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
            mail.IsBodyHtml = true;
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
            SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
            SmtpServer.EnableSsl = true;
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPLogin"], ConfigurationManager.AppSettings["SMTPPassword"]);
            SmtpServer.Send(mail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mTo"></param>
        /// <param name="mSubject"></param>
        /// <param name="mBody"></param>
        /// <param name="SystemName"></param>
        /// <param name="mCC"></param>
        /// <param name="mBCC"></param>
        /// <param name="attachmentsFiles"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool SendMail(string mTo, string mSubject, string mBody, string SystemName = "SmartStock", string mCC = "", string mBCC = "", string attachmentsFiles = "", bool isHTML = true)
        {
            if (string.IsNullOrEmpty(mTo) | string.IsNullOrEmpty(mSubject) | string.IsNullOrEmpty(mBody))
                return false;


            System.Net.Mail.SmtpClient SmtpClient = new System.Net.Mail.SmtpClient();
            MailMessage message = new MailMessage();

            try
            {


                // MailAddress fromAddress = new MailAddress(txtEmail.Text, txtName.Text)

                // You can specify the host name or ipaddress of your server
                // Default in IIS will be localhost 
                //var configurationFile = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath + "/Web.config");
                //MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings");

                //int port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                //string host = ConfigurationManager.AppSettings["SMTPServer"];
                //string password = ConfigurationManager.AppSettings["SMTPPassword"];
                //string username = ConfigurationManager.AppSettings["SMTPLogin"];
                //string From = ConfigurationManager.AppSettings["EmailSender"];

                int port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                string host = ConfigurationManager.AppSettings["SMTPServer"];
                string password = ConfigurationManager.AppSettings["SMTPPassword"];
                string username = ConfigurationManager.AppSettings["SMTPLogin"];
                string From = ConfigurationManager.AppSettings["EmailSender"];
                string SenderName = ConfigurationManager.AppSettings["EmailSenderName"];


                SmtpClient.Host = host;
                SmtpClient.Port = port; // 587

                if (!(attachmentsFiles == null) && !(attachmentsFiles == ""))
                {
                    string[] Attach = attachmentsFiles.Split(new char[] { ';' });
                    var loopTo = Attach.Count() - 1;
                    for (int n = 0; n <= loopTo; n++)
                    {
                        if (!string.IsNullOrEmpty(Attach[n]))
                            message.Attachments.Add(new System.Net.Mail.Attachment(Attach[n].Replace("/", @"\"), "text/html"));
                    }
                }

                SmtpClient.Credentials = new System.Net.NetworkCredential(username, password);

                SmtpClient.EnableSsl = false;
                // Default port will be 25

                // From address will be given as a MailAddress Object
                message.From = new MailAddress(From, SystemName == "" ? SenderName : SystemName);

                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                // To address collection of MailAddress
                string[] Tos = mTo.Split(new char[] { ';' });
                var loopTo1 = Tos.Count() - 1;
                for (int n = 0; n <= loopTo1; n++)
                {
                    if (!string.IsNullOrEmpty(Tos[n]))
                        message.To.Add(Tos[n]);
                }
                message.Subject = mSubject;

                // CC and BCC optional
                // MailAddressCollection class is used to send the email to various users
                // You can specify Address as new MailAddress("admin1@yoursite.com")
                if (!(mCC == null))
                {
                    string[] CCs = mCC.Split(new char[] { ';' }); ;
                    var loopTo2 = CCs.Count() - 1;
                    for (int n = 0; n <= loopTo2; n++)
                    {
                        if (!string.IsNullOrEmpty(CCs[n]))
                            message.CC.Add(CCs[n]);
                    }
                }

                // You can specify Address directly as string
                if (!(mBCC == null))
                {
                    string[] BCCs = mBCC.Split(new char[] { ';' }); ;
                    var loopTo3 = BCCs.Count() - 1;
                    for (int n = 0; n <= loopTo3; n++)
                    {
                        if (!string.IsNullOrEmpty(BCCs[n]))
                            message.Bcc.Add(BCCs[n]);
                    }
                }
                // Body can be Html or text format
                // Specify true if it  is html message
                message.IsBodyHtml = isHTML;



                message.Body = mBody;

                // Send SMTP mail
                SmtpClient.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        
    }
}