namespace BarberShop.Modules.Notifications.Api.Template.Factory;

internal sealed class OrderCreatedTemplate : EmailTemplate
{
    private Guid OrderId { get;}
    private DateTime DeliveryTime {  get;}

    public OrderCreatedTemplate(Guid orderId, DateTime deliveryTime)
    {
        OrderId = orderId;
        DeliveryTime = deliveryTime;
    }

    public override async Task<string> GetBodyEmail()
    {
        var path = GetPath();
        var template = await File.ReadAllTextAsync(path);
        
        var document = template
            .Replace("{orderId}", OrderId.ToString())
            .Replace("{date}", DeliveryTime.Date.ToShortDateString());
        
        return document;
    }
}