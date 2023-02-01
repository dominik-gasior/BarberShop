using Application.ServiceManager;
using FastEndpoints;
using Src.Domain;
using Order = Src.Domain.Order;

namespace Application.Features.ClientFeatures.Query;

public record GetClientByNumberPhoneRequest{public string numberPhone { get; init; }}

public record struct GetClientByNumberPhoneResponse(int Id, string FirstName, string LastName, string NumberPhone,
    string Email, IEnumerable<Visit> Visits, IEnumerable<Order> Orders);

public class
    GetClientByNumberPhoneMapperProfile : Mapper<GetClientByNumberPhoneRequest, GetClientByNumberPhoneResponse, Client>
{
    public override GetClientByNumberPhoneResponse FromEntity(Client e)
        => new()
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            Orders = e.Orders,
            NumberPhone = e.NumberPhone,
            Visits = e.Visits
        };
}
public class GetClientByNumberPhoneEndpoint : Endpoint<GetClientByNumberPhoneRequest, GetClientByNumberPhoneResponse, GetClientByNumberPhoneMapperProfile>
{
    private readonly IServiceManager _serviceManager;

    public GetClientByNumberPhoneEndpoint(IServiceManager serviceManager) => _serviceManager = serviceManager;

    public override void Configure()
    {
        Get("/api/client/numberPhone/{numberPhone}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetClientByNumberPhoneRequest req, CancellationToken ct)
        => await SendAsync(
            Map.FromEntity(await _serviceManager.ClientService.GetClientByNumberPhone(req.numberPhone, ct)), cancellation: ct);
}