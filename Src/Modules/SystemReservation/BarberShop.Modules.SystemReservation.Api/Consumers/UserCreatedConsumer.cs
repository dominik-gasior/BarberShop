using BarberShop.Modules.SystemReservation.Api.Entities;
using BarberShop.Modules.SystemReservation.Api.Persistence;
using BarberShop.Modules.Users.Shared.Event;
using MassTransit;

namespace BarberShop.Modules.SystemReservation.Api.Consumers;

public sealed class UserCreatedConsumer :  IConsumer<UserCreated>
{
    private readonly SystemReservationDbContext _dbContext;
    
    public UserCreatedConsumer(SystemReservationDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        var data = context.Message;
        if (data.IsClient)
        {
            var client = new Client
            {   
                Id = data.Id,
                Fullname = data.Fullname,
                Email = data.Email,
                NumberPhone = data.NumberPhone
            };
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();  
        }
        else
        {
            var employee = new Employee
            {   
                Id = data.Id,
                Fullname = data.Fullname,
            };
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }
        
    }
}