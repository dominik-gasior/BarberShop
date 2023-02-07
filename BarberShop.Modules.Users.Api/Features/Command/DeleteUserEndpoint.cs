using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Command;

public record DeleteUserRequest { public int Id { get; init; } }
public record DeleteUserResponse(string Message);

public class DeleteUserEndpoint : Endpoint<DeleteUserRequest, DeleteUserResponse>
{
    private readonly IUserService _userService;
    public DeleteUserEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Delete("/api/deleteUser/{Id}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
        => await SendAsync(new DeleteUserResponse(await _userService.DeleteUser(req.Id, ct)), cancellation: ct);
}