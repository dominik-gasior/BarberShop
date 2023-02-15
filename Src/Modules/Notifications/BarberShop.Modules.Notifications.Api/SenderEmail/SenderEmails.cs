using System.Net;
using System.Net.Mail;

namespace BarberShop.Modules.Notifications.Api.SenderEmail;

public class SenderEmails
{
    //TODO send custom mail
    public void SendEmail()
    {
        try
        {
            using MailMessage mail = new MailMessage();
            mail.From = new MailAddress("barbershopsender@onet.pl");
            mail.To.Add("kontotestowedominik@gmail.com");
            mail.Subject = "Test";
            mail.Body = "<p>Test</p>";
            mail.IsBodyHtml = true;
            using (SmtpClient smtpClient = new SmtpClient("smtp.poczta.onet.pl", 587))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(LoginSenderData.LoginEmail, LoginSenderData.PasswordEmail);
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);
                Console.WriteLine("Udało się");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}