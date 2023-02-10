using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Query;

internal sealed record GetAllUsersResponse(int Id, string FirstName, string LastName, string Email, string NumberPhone);
internal sealed class GetAllUsersMapperProfile : ResponseMapper<IEnumerable<GetAllUsersResponse>, IEnumerable<User>>
{
    public override IEnumerable<GetAllUsersResponse> FromEntity(IEnumerable<User> c)
        => c.Select(r => new GetAllUsersResponse(
            r.Id,
            r.FirstName,
            r.LastName,
            r.Email,
            r.NumberPhone
        ));
}
internal sealed class GetAllUsersEndpoint : EndpointWithoutRequest<IEnumerable<GetAllUsersResponse>,GetAllUsersMapperProfile>
{
    private readonly IUserService _userService;
    public GetAllUsersEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Get("/api/getAllUsers");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _userService.GetAllUsers()), cancellation: ct);
}