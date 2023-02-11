using BarberShop.Modules.SystemReservation.Api.Entities;
using BarberShop.Modules.SystemReservation.Api.Persistence;
using BarberShop.Modules.Users.Shared.Event;
using MassTransit;

namespace BarberShop.Modules.SystemReservation.Api.Consumers;

public sealed class UserConsumer :  IConsumer<UserCreated>
{
    private readonly ISystemReservationRepository _systemReservationRepository;
    
    public UserConsumer(ISystemReservationRepository systemReservationRepository)
        => _systemReservationRepository  = systemReservationRepository;
    
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        var data = context.Message;
        var client = new Client
        {   
            Id = data.Id,
            Fullname = data.Fullname,
            Email = data.Email,
            NumberPhone = data.NumberPhone
        };
        await _systemReservationRepository.InsertClient(client);
        await _systemReservationRepository.SaveChangesAsync();
    }
}