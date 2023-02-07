using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Command;

public record UpdateUserRequest { public int Id { get; init; } public string? Email { get; init; } public string? NumberPhone { get; init; } }

public record UpdateUserResponse(string Message);
public class UpdateUserEndpoint : Endpoint<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IUserService _userService;
    public UpdateUserEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Put("/api/updateUser");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
        => await SendAsync(new UpdateUserResponse(await _userService.UpdateUser(req)), cancellation: ct);
}