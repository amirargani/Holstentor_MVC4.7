using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Restaurant.Models.Plugin
{
    public class Email
    {
        public void SendEmail(string email, string subject, string message, string emailpost)
        {
            MailMessage msg = new MailMessage();
            msg.Body = message;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(emailpost, "Restaurant am Holstentor", Encoding.UTF8);
            msg.Priority = MailPriority.Normal;
            msg.Sender = msg.From;
            msg.Subject = subject;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.To.Add(new MailAddress(email, email, Encoding.UTF8));

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.ionos.de";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(emailpost, "pass");

            smtp.Send(msg);
        }
    }
}
