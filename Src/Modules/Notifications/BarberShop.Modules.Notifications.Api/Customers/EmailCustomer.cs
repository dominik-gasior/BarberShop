using BarberShop.Modules.Notifications.Api.SMTP;
using BarberShop.Modules.Notifications.Api.Template;
using BarberShop.Modules.Users.Shared.Event;
using MassTransit;

namespace BarberShop.Modules.Notifications.Api.Customers;

public sealed class EmailCustomer : IConsumer<UserCreated>
{
    //TODO Do a email sender for visit/order/deleted etc.
    //This customer is example.
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        var sender = new SenderEmails();
        var template = new EmailTemplate();
        var body = await template.GetBodyEmail(context.Message.Fullname);
        sender.SendEmail("Thank you for register our services!", body);
    }
}