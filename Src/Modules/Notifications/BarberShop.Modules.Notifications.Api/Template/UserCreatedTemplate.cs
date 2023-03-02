namespace BarberShop.Modules.Notifications.Api.Template;

internal sealed class UserCreatedTemplate : EmailTemplate
{
    public UserCreatedTemplate(string fullName) => FullName = fullName;
    private string FullName { get;}
    public override async Task<string> GetBodyEmail()
    {
        var path = GetPath();
        var template = await File.ReadAllTextAsync(path);
        var document = template.Replace("{name}", FullName);
        return document;
    }
}