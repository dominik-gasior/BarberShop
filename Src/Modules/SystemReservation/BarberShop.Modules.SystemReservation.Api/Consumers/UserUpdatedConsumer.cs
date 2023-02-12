using BarberShop.Modules.SystemReservation.Api.Entities;
using BarberShop.Modules.SystemReservation.Api.Persistence;
using BarberShop.Modules.Users.Shared.Event;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.SystemReservation.Api.Consumers;

public class UserUpdatedConsumer : IConsumer<UserUpdated>
{
    private readonly SystemReservationDbContext _dbContext;

    public UserUpdatedConsumer(SystemReservationDbContext dbContext)
        => _dbContext = dbContext;

    public async Task Consume(ConsumeContext<UserUpdated> context)
    {
        var data = context.Message;
        if (data.IsClient)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id.Equals(data.Id));
            if (client is not null)
            {
                client.UpdateEmail(data.Email);
                client.UpdateNumberPhone(data.NumberPhone);
                _dbContext.Clients.Update(client);
                await _dbContext.SaveChangesAsync();
            } 
        }
    }
}