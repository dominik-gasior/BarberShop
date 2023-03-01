using BarberShop.Modules.Users.Shared.Event;
using BarberShop.Modules.Warehouse.Api.Entities;
using BarberShop.Modules.Warehouse.Api.Persistence;
using MassTransit;

namespace BarberShop.Modules.Warehouse.Api.Consumers;

public sealed class UserCreatedConsumer :  IConsumer<UserCreated>
{
    private readonly WarehouseDbContext _dbContext;
    
    public UserCreatedConsumer(WarehouseDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        var data = context.Message;
        if (data.IsClient)
        {
            var client = new Client
            {   
                Id = data.Id,
                FullName = data.Fullname,
                Email = data.Email,
                NumberPhone = data.NumberPhone
            };
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();  
        }
    }
}