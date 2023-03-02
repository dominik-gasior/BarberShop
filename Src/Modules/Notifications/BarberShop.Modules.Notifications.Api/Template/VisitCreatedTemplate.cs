namespace BarberShop.Modules.Notifications.Api.Template.Factory;

internal sealed class VisitCreatedTemplate : EmailTemplate
{
    private string FullName { get; }
    private DateTime DateTime { get; }
    
    public VisitCreatedTemplate(string fullName, DateTime dateTime)
    {
        FullName = fullName;
        DateTime = dateTime;
    }
    public override async Task<string> GetBodyEmail()
    {
        var path = GetPath();
        var template = await File.ReadAllTextAsync(path);
        
        var document = template
            .Replace("{name}", FullName)
            .Replace("{date}", DateTime.Date.ToShortDateString())
            .Replace("{time}", DateTime.ToString("t"));
        
        return document;
    }
}