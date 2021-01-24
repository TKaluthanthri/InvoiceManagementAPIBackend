
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mail;

namespace InvoiceManagementAPI.EmailSendingService
{
    public static class EmailService
    {

        public static bool SendInvoiceAsSMTPEmail(EmailModel emailModel, EmailBodyContent emailBody) {

            bool status = false;

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 587, false);
                smtpClient.Authenticate(ConfigurationManager.AppSettings["smtpEmail"].ToString(), ConfigurationManager.AppSettings["smtpPassword"].ToString());

                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress("Email Sender", emailModel.EmailSender));
                mailMessage.To.Add(new MailboxAddress("Receiver", emailModel.EmailReceiver));
                mailMessage.Subject = emailModel.EmailSubject;

                if (emailBody != null)
                {
                    emailModel.EmailBody = emailModel.EmailBody.Replace("{{FirstName}}", emailBody.FirstName ?? "");
                    emailModel.EmailBody = emailModel.EmailBody.Replace("{{LastName}}", emailBody.LastName ?? "");
                    emailModel.EmailBody = emailModel.EmailBody.Replace("{{Date}}", emailBody.Date ?? "");
                    emailModel.EmailBody = emailModel.EmailBody.Replace("{{Invoice Number}}", emailBody.InvoiceNumber ?? "");
                    emailModel.EmailBody = emailModel.EmailBody.Replace("{{Amount}}", emailBody.Amount ?? "");
                    emailModel.EmailBody = emailModel.EmailBody.Replace("{{Address}}", emailBody.Address ?? "");
                    
                }
                mailMessage.Body = new TextPart("plain")
                {
                    Text = emailModel.EmailBody
                };

                try
                {
                    smtpClient.Send(mailMessage);
                    smtpClient.Disconnect(true);
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                }
            };
            return status;
        }

       
    }
}