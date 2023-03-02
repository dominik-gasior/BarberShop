using BarberShop.Modules.Notifications.Api.SMTP;
using BarberShop.Modules.Notifications.Api.Template;
using BarberShop.Modules.Users.Shared.Event;
using BarberShop.Modules.Warehouse.Shared.Event;
using MassTransit;

namespace BarberShop.Modules.Notifications.Api.Customers;

public class OrderCustomer : IConsumer<OrderCreated>
{
    //TODO Do a email sender for visit/order/deleted etc.
    //This customer is example.
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var sender = new SenderEmails();
        var template = new EmailTemplate();
        var body = await template.GetBodyEmail(context.Message.OrderId,context.Message.DeliveryTime);
        sender.SendEmail("Thank you for ordered our products!", body);
    }
}