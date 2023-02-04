using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Command;

public record DeleteClientRequest { public int Id { get; init; } }
public record struct DeleteClientResponse(string Message);

public class DeleteClientEndpoint : Endpoint<DeleteClientRequest, DeleteClientResponse>
{
    private readonly IUserService _userService;
    public DeleteClientEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Delete("/api/deleteClient/{Id}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(DeleteClientRequest req, CancellationToken ct)
        => await SendAsync(new DeleteClientResponse(await _userService.DeleteUser(req.Id, ct)), cancellation: ct);
}