using System.Net;
using System.Net.Mail;

namespace BarberShop.Modules.Notifications.Api;

public class SenderEmail
{
    public void SendEmail()
    {
        try
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("");
                mail.To.Add("");
                mail.Subject = "";
                mail.Body = "";
                mail.IsBodyHtml = true;

                using (SmtpClient smtpClient = new SmtpClient("", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("login", "pass");
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mail);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}