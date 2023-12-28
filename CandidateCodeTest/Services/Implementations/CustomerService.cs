using CandidateCodeTest.Common.Interfaces;
using CandidateCodeTest.Repository;
using CandidateCodeTest.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace CandidateCodeTest
{
    public class CustomerService : ICustomerService
    {
        public ILogWriter _logWriter;
        public IMessageService _messageService;
        public IMessageRepository _messageRepository;
        public TimeSpan _startTime;
        public TimeSpan _endTime;
        public CustomerService(IMessageService messageService, IMessageRepository messageRepository, TimeSpan startTime, TimeSpan endTime, ILogWriter logWriter)
        {
            _logWriter = logWriter;
            _startTime = startTime;
            _endTime = endTime;
            _messageRepository = messageRepository;
            _messageService = messageService;
        }
        public bool HasEmailBeenSent()
        {
            _logWriter.LogWrite("HasEmailBeenSent Method Started");
            MessageService messageServices = new MessageService(_logWriter);
            try
            {
                TimeSpan now = DateTime.Now.TimeOfDay;
                if ((now > _startTime) && (now < _endTime))
                {
                    messageServices.SendEmail();
                    _logWriter.LogWrite("HasEmailBeenSent Method ended");
                    return true;
                }
                else
                {
                    _logWriter.LogWrite("HasEmailBeenSent Method ended, email not sent due to ime constratint");
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logWriter.LogWrite($"HasEmailBeenSent Method ended, email not sent, exception message : {ex.Message}");
                return false;
            }
        }
    }

    public class MessageService : IMessageService
    {
        public ILogWriter _logWriter;
        public MessageService(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }
        public void SendEmail()
        {
            // Code that will send the email
            int retry = 3;
            bool failed;
            do
            {
                try
                {
                    failed = false;
                    MailMessage myMail = EmailMessage();
                    SmtpMail(myMail);
                    break; // Break if sending is successful
                }
                catch (SmtpException ex)
                {
                    _logWriter.LogWrite($"SendEmail Failed, Retry Number: {retry}, SmtpException: {ex.Message}");
                    failed = true;
                    retry--;
                }
                catch (Exception ex)
                {
                    _logWriter.LogWrite($"SendEmail Failed, Retry Number: {retry}, Exception Message: {ex.Message}");
                    failed = true;
                    retry--;
                }
                Thread.Sleep(5000); // adding delay of 5 secs, before doing next retry
            }
            while (failed && retry != 0);
        }

        private static void SmtpMail(MailMessage myMail)
        {
            using SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com", 465);
            mySmtpClient.UseDefaultCredentials = false;
            mySmtpClient.Credentials = new NetworkCredential("emailassignmenttest@gmail.com", "Email@123");
            mySmtpClient.EnableSsl = true;
            mySmtpClient.Send(myMail);
        }

        private static MailMessage EmailMessage()
        {
            MailAddress from = new MailAddress("emailassignmenttest@gmail.com", "EMail test");
            MailAddress to = new MailAddress("mir.javid3822@gmail.com", "Javid");
            MailMessage myMail = new MailMessage(from, to)
            {
                // set subject and encoding
                Subject = "Test message",
                SubjectEncoding = System.Text.Encoding.UTF8,
                // set body-message and encoding
                Body = "<b>Test Mail</b><br>using <b>HTML</b>.",
                BodyEncoding = System.Text.Encoding.UTF8,
                // text or html
                IsBodyHtml = true
            };
            return myMail;
        }
    }
}
