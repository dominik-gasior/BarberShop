using BarberShop.Modules.Users.Shared.Event;
using BarberShop.Modules.Warehouse.Api.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.Warehouse.Api.Consumers;

public sealed class UserDeletedConsumer : IConsumer<UserDeleted>
{
    private readonly WarehouseDbContext _dbContext;

    public UserDeletedConsumer(WarehouseDbContext dbContext)
        => _dbContext = dbContext;
    

    public async Task Consume(ConsumeContext<UserDeleted> context)
    {
        var data = context.Message;
        if (data.IsClient)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id.Equals(data.Id));
            if (client is not null)
            {
                _dbContext.Clients.Remove(client);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}