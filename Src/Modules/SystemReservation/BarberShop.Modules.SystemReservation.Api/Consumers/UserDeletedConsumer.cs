using BarberShop.Modules.SystemReservation.Api.Persistence;
using BarberShop.Modules.Users.Shared.Event;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.SystemReservation.Api.Consumers;

public class UserDeletedConsumer : IConsumer<UserDeleted>
{
    private readonly SystemReservationDbContext _dbContext;

    public UserDeletedConsumer(SystemReservationDbContext dbContext)
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
        else
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(c => c.Id.Equals(data.Id));
            if (employee is not null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}