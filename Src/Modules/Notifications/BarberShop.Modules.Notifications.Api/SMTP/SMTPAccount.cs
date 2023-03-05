using Microsoft.Extensions.Configuration;

namespace BarberShop.Modules.Notifications.Api.SMTP;

public static class SmtpAccount
{
    public static string Email { get; set; } = "";
    public static string Password { get; set; } = "";

}