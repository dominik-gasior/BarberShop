namespace BarberShop.Modules.Notifications.Api.Template;

public abstract class EmailTemplate
{
    public abstract Task<string> GetBodyEmail();
    protected string GetPath()
        => Path.Combine(Environment.CurrentDirectory,
            "..",
            "..",
            "Modules/Notifications/BarberShop.Modules.Notifications.Api/Template/HTML/",
            $"{GetType().Name}.txt"
            );
    
}