using BarberShop.Modules.Notifications.Api.SMTP;
using BarberShop.Modules.Notifications.Api.Template;
using BarberShop.Modules.SystemReservation.Shared.Event;
using MassTransit;

namespace BarberShop.Modules.Notifications.Api.Customers;

public sealed class VisitCustomer : IConsumer<VisitCreated>
{
    //TODO Do a email sender for visit/order/deleted etc.
    //This customer is example.
    public async Task Consume(ConsumeContext<VisitCreated> context)
    {
        var sender = new SenderEmails();
        var template = new EmailTemplate();
        var body = await template.GetBodyEmail(context.Message.Fullname, context.Message.Date);
        sender.SendEmail("Thank you for reservation visit!", body);
    }
}