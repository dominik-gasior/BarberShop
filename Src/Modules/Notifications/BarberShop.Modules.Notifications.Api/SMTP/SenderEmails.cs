using System.Net;
using System.Net.Mail;

namespace BarberShop.Modules.Notifications.Api.SMTP;

public sealed class SenderEmails
{
    //TEST VERSION
    //TODO send custom mail
    public void SendEmail(string subject, string body)
    {
        try
        {
            using MailMessage mail = new MailMessage();
            mail.From = new MailAddress("barbershopsender@onet.pl");
            
            mail.To.Add("barbershopsender@onet.pl");
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            
            using SmtpClient smtpClient = new SmtpClient("smtp.poczta.onet.pl", 587);
            
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(LoginSenderData.LoginEmail, LoginSenderData.PasswordEmail);
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);
            Console.WriteLine("Email sent!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}