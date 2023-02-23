using BarberShop.Modules.SystemReservation.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.SystemReservation.Api.Features.Query;

internal sealed record GetAllServiceResponse(int Id,string Name,decimal Price, int Time);

internal sealed class GetAllServiceMapperProfile : ResponseMapper<IEnumerable<GetAllServiceResponse>, IEnumerable<ServiceIndustry>>
{
    public override IEnumerable<GetAllServiceResponse> FromEntity(IEnumerable<ServiceIndustry> e)
        => e.Select(s => new GetAllServiceResponse
        (
            s.Id,
            s.Name,
            s.Price,
            s.Time
        ));
}
internal sealed class GetAllServiceEndpoint : EndpointWithoutRequest<IEnumerable<GetAllServiceResponse>, GetAllServiceMapperProfile>
{
    private readonly ISystemReservationService _systemReservationService;

    public GetAllServiceEndpoint(ISystemReservationService systemReservationService)
        => _systemReservationService = systemReservationService;
    
    public override void Configure()
    {
        Get("/api/getAllServiceIndustry");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _systemReservationService.GetAllService()), cancellation: ct);
}