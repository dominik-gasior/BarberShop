using BarberShop.Modules.Notifications.Api.SenderEmail;
using BarberShop.Modules.Users.Shared.Event;
using MassTransit;

namespace BarberShop.Modules.Notifications.Api.Customers;

public sealed class EmailCustomer : IConsumer<UserCreated>
{
    public Task Consume(ConsumeContext<UserCreated> context)
    {
        var sender = new SenderEmails();
        sender.SendEmail();
        return Task.CompletedTask;
    }
}