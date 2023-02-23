using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Query;

internal sealed record GetAllClientsResponse(Guid Id, string FirstName, string LastName, string Email, string NumberPhone);
internal sealed class GetAllClientsMapperProfile : ResponseMapper<IEnumerable<GetAllClientsResponse>, IEnumerable<User>>
{
    public override IEnumerable<GetAllClientsResponse> FromEntity(IEnumerable<User> c)
        => c.Select(r => new GetAllClientsResponse(
            r.Id,
            r.FirstName,
            r.LastName,
            r.Email,
            r.NumberPhone
        ));
}
internal sealed class GetAllClientsEndpoint : EndpointWithoutRequest<IEnumerable<GetAllClientsResponse>,GetAllClientsMapperProfile>
{
    private readonly IUserService _userService;
    public GetAllClientsEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Get("/api/getAllClients");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _userService.GetAllClients()), cancellation: ct);
}