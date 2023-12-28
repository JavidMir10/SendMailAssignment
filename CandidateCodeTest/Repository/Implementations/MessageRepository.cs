using CandidateCodeTest.Common.Interfaces;
using System;
using System.Net;
using System.Net.Mail;

namespace CandidateCodeTest.Repository.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        public ILogWriter _logWriter;
        public MessageRepository(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }
        public bool SendEmail()
        {
            try
            {
                //SendMail();
                //mySmtpClient.Port = 465;
                SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com", 465)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("emailassignmenttest@gmail.com", "Email@123"),
                    EnableSsl = true
                };
                // add from,to mailaddresses
                MailAddress from = new MailAddress("emailassignmenttest@gmail.com", "EMail test");
                MailAddress to = new MailAddress("mir.javid3822@gmail.com", "Javid");
                MailMessage myMail = new MailMessage(from, to)
                {

                    // add ReplyTo
                    //MailAddress replyTo = new MailAddress("emailassignmenttest@gmail.com");
                    //myMail.ReplyToList.Add(replyTo);

                    // set subject and encoding
                    Subject = "Test message",
                    SubjectEncoding = System.Text.Encoding.UTF8,

                    // set body-message and encoding
                    Body = "<b>Test Mail</b><br>using <b>HTML</b>.",
                    BodyEncoding = System.Text.Encoding.UTF8,
                    // text or html
                    IsBodyHtml = true
                };
                mySmtpClient.Send(myMail);
                return true;
            }

            catch (SmtpException ex)
            {
                return false;
                //throw new ApplicationException
                //  ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        #region TrailRegion

        //public static void SendMail()
        //{
        //    //make this smtpClient global. Because you will use next time.
        //    SmtpClient smtpClient = new SmtpClient();
        //    smtpClient.Credentials = new System.Net.NetworkCredential("smtpUserName", "smtpPassword");
        //    smtpClient.Host = "mail.mailhost.com";//set your smtp host. Generally mail.domain.com
        //    smtpClient.Port = 587;//set your smtp port
        //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpClient.EnableSsl = false;//you can change this based on your settings

        //    MailMessage mail = new MailMessage();
        //    mail.From = new MailAddress("mir.javid3822@gmail.com", "javid");
        //    mail.To.Add(new MailAddress("fouldraker@gmail.com", "toFould"));
        //    //for (int i = 0; i < emaillAddresses.Count; i++)
        //    //{
        //    //    mail.Bcc.Add(new MailAddress(emaillAddresses[i]));
        //    //}
        //    mail.Subject = "Wish you a very happy new year";
        //    mail.IsBodyHtml = true;
        //    mail.BodyEncoding = System.Text.Encoding.UTF8;
        //    smtpClient.Send(mail);
        //}

        public int Add(int a, int b)
        {
            return a + b;
        }
        #endregion
    }
}
