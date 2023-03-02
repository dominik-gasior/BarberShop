using BarberShop.Modules.Notifications.Api.SMTP;
using BarberShop.Modules.Notifications.Api.Template;
using BarberShop.Modules.Warehouse.Shared.Event;
using MassTransit;

namespace BarberShop.Modules.Notifications.Api.Customers;

public class OrderStatusConsumer : IConsumer<OrderStatusChanged>, INotificationConsumer
{

    public async Task Consume(ConsumeContext<OrderStatusChanged> context)
    {
        var template = new OrderStatusChangedTemplate(context.Message.OrderId, context.Message.OrderStatus);
        var body = await template.GetBodyEmail();
        var sender = new SenderEmails();
        sender.SendEmail("Change status your order", body);        
    }
}