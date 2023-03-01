using BarberShop.Modules.Users.Shared.Event;
using BarberShop.Modules.Warehouse.Api.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.Warehouse.Api.Consumers;

public sealed class UserUpdatedConsumer : IConsumer<UserUpdated>
{
    private readonly WarehouseDbContext _dbContext;

    public UserUpdatedConsumer(WarehouseDbContext dbContext)
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