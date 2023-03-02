namespace BarberShop.Modules.Notifications.Api.Template;

internal sealed class OrderStatusChangedTemplate : EmailTemplate
{
    private Guid OrderId { get; }
    private string OrderStatus { get; }
    public OrderStatusChangedTemplate(Guid orderId, string orderStatus)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
    }

    public override async Task<string> GetBodyEmail()
    {
        var path = GetPath();
        var template = await File.ReadAllTextAsync(path);
        
        var document = template
            .Replace("{OrderId}", OrderId.ToString())
            .Replace("{OrderStatus}", OrderStatus);
        
        return document;
    }
}