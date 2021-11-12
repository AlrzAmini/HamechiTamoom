using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace HamechiTamoom.Core.Senders 
{
    public class SendEmail
    {
        public static void Send(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.mail.yahoo.com");
            mail.From = new MailAddress("hmechitamoom@yahoo.com", "همه چی تموم");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("hmechitamoom@yahoo.com", "jhtfzwsspdcqqpqo");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
}