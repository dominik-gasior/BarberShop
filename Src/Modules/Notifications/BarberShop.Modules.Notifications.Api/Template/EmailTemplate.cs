namespace BarberShop.Modules.Notifications.Api.Template;

internal sealed class EmailTemplate 
{
    public async Task<string> GetBodyEmail(string fullName)
    {
        var template = await File.ReadAllTextAsync(@"/Users/dominikgasior/RiderProjects/BarberShop/Src/Modules/Notifications/BarberShop.Modules.Notifications.Api/Template/HTML/UserCreatedTemplate.txt");
        var document = template.Replace("{name}", fullName);
        return document;
    }
    public async Task<string> GetBodyEmail(string fullName, DateTime dateTime)
    {
        var template = await File.ReadAllTextAsync(@"/Users/dominikgasior/RiderProjects/BarberShop/Src/Modules/Notifications/BarberShop.Modules.Notifications.Api/Template/HTML/VisitCreatedTemplate.txt");
        var document = template
            .Replace("{name}", fullName)
            .Replace("{date}", dateTime.Date.ToShortDateString())
            .Replace("{time}", dateTime.ToString("t"));
        return document;
    }
}