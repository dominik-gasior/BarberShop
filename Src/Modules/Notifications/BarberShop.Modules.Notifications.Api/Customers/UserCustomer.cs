using BarberShop.Modules.Notifications.Api.SMTP;
using BarberShop.Modules.Notifications.Api.Template;
using BarberShop.Modules.Notifications.Api.Template.Factory;
using BarberShop.Modules.Users.Shared.Event;
using MassTransit;

namespace BarberShop.Modules.Notifications.Api.Customers;

public sealed class UserCustomer : IConsumer<UserCreated>, INotificationConsumer
{
    //TODO Do a email sender for visit/order/deleted etc.
    //This customer is example.
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        var template = new UserCreatedTemplate(context.Message.Fullname);
        var body = await template.GetBodyEmail();
        var sender = new SenderEmails();
        sender.SendEmail("Thank you for register our services!", body);
    }
}