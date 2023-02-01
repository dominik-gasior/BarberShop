using Application.ServiceManager;
using FastEndpoints;
using Src.Domain;
using Order = Src.Domain.Order;

namespace Application.Features.ClientFeatures.Query;

public record GetClientByIdRequest { public int Id { get; init; } }
public record struct GetClientByIdResponse(int Id, string FirstName, string LastName, string NumberPhone, string Email, IEnumerable<Visit> Visits, IEnumerable<Order> Orders);

public class GetClientByIdMapperProfile : Mapper<GetClientByIdRequest, GetClientByIdResponse, Client>
{
    public override GetClientByIdResponse FromEntity(Client e)
        => new()
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            NumberPhone = e.NumberPhone,
            Orders = e.Orders,
            Visits = e.Visits
        };
}
public class GetClientByIdEndpoint : Endpoint<GetClientByIdRequest, GetClientByIdResponse, GetClientByIdMapperProfile>
{
    private readonly IServiceManager _serviceManager;

    public GetClientByIdEndpoint(IServiceManager serviceManager) => _serviceManager = serviceManager;

    public override void Configure()
    {
        Get("/api/client/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetClientByIdRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _serviceManager.ClientService.GetClientById(req.Id, ct)), cancellation: ct);
}