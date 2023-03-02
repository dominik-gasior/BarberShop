using BarberShop.Modules.Notifications.Api.SMTP;
using BarberShop.Modules.Notifications.Api.Template;
using BarberShop.Modules.Notifications.Api.Template.Factory;
using BarberShop.Modules.Users.Shared.Event;
using BarberShop.Modules.Warehouse.Shared.Event;
using MassTransit;

namespace BarberShop.Modules.Notifications.Api.Customers;

public class OrderCustomer : IConsumer<OrderCreated>, INotificationConsumer
{
    //TODO Do a email sender for visit/order/deleted etc.
    //This customer is example.
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var template = new OrderCreatedTemplate(context.Message.OrderId,context.Message.DeliveryTime);
        var body = await template.GetBodyEmail();
        var sender = new SenderEmails();
        sender.SendEmail("Thank you for ordered our products!", body);
    }
}