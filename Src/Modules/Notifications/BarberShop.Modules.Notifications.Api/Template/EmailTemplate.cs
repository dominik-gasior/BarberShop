namespace BarberShop.Modules.Notifications.Api.Template;

public class EmailTemplate 
{
    public async Task<string> GetBodyEmail(string fullName)
    {
        var template = await File.ReadAllTextAsync(@"/Users/dominikgasior/RiderProjects/BarberShop/Src/Modules/Notifications/BarberShop.Modules.Notifications.Api/Template/HTML/UserCreatedTemplate.txt");
        var document = template.Replace("{name}", fullName);
        return document;
    }
}