using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Command;

public record CreateNewUserRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string NumberPhone { get; init; }
    public string? Email { get; init; }
}
public class CreateNewUserMapperProfile : RequestMapper<CreateNewUserRequest, User>
{
    public override User ToEntity(CreateNewUserRequest r) => new(0, r.FirstName, r.LastName, r.NumberPhone, r.Email, (int)Role.Klient);
}
public class CreateNewUserEndpoint : Endpoint<CreateNewUserRequest, string, CreateNewUserMapperProfile>
{
    private readonly IUserService _userService;
    public CreateNewUserEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Post("/api/createNewUser");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CreateNewUserRequest req, CancellationToken ct)
        => await SendAsync(await _userService.CreateNewUser(Map.ToEntity(req)), cancellation: ct);
}