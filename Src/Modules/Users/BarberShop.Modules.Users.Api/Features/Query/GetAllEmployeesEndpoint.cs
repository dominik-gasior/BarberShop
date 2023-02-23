using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Query;

internal sealed record GetAllEmployeesResponse(Guid Id, string FirstName, string LastName, string Email, string NumberPhone);
internal sealed class GetAllEmployeesMapperProfile : ResponseMapper<IEnumerable<GetAllEmployeesResponse>, IEnumerable<User>>
{
    public override IEnumerable<GetAllEmployeesResponse> FromEntity(IEnumerable<User> c)
        => c.Select(r => new GetAllEmployeesResponse(
            r.Id,
            r.FirstName,
            r.LastName,
            r.Email,
            r.NumberPhone
        ));
}
internal sealed class GetAllEmployeesEndpoint : EndpointWithoutRequest<IEnumerable<GetAllEmployeesResponse>,GetAllEmployeesMapperProfile>
{
    private readonly IUserService _userService;
    public GetAllEmployeesEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Get("/api/getAllClients");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _userService.GetAllEmployees()), cancellation: ct);
}