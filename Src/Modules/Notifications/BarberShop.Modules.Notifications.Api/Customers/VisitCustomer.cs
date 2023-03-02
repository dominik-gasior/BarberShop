using BarberShop.Modules.Notifications.Api.SMTP;
using BarberShop.Modules.Notifications.Api.Template.Factory;
using BarberShop.Modules.SystemReservation.Shared.Event;
using MassTransit;

namespace BarberShop.Modules.Notifications.Api.Customers;

public sealed class VisitCustomer : IConsumer<VisitCreated>, INotificationConsumer
{
    //TODO Do a email sender for visit/order/deleted etc.
    //This customer is example.
    public async Task Consume(ConsumeContext<VisitCreated> context)
    {
        var template = new VisitCreatedTemplate(context.Message.Fullname, context.Message.Date);
        var body = await template.GetBodyEmail();
        var sender = new SenderEmails();
        sender.SendEmail("Thank you for reservation visit!", body);
    }
}