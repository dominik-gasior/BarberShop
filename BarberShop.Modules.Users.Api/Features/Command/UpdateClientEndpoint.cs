using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Command;

public record UpdateClientRequest { public int Id { get; init; } public string? Email { get; init; } public string? NumberPhone { get; init; } }

public record UpdateClientResponse(string Message);
public class UpdateClientEndpoint : Endpoint<UpdateClientRequest, UpdateClientResponse>
{
    private readonly IUserService _userService;
    public UpdateClientEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Put("/api/updateClient");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateClientRequest req, CancellationToken ct)
        => await SendAsync(new UpdateClientResponse(await _userService.UpdateUser(req,ct)), cancellation: ct);
}