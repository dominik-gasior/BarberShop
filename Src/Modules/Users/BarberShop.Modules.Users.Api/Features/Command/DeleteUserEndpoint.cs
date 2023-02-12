using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Command;

internal sealed record DeleteUserRequest { public Guid UserId { get; init; } }
internal sealed record DeleteUserResponse(string Message);

internal sealed class DeleteUserEndpoint : Endpoint<DeleteUserRequest, DeleteUserResponse>
{
    private readonly IUserService _userService;
    public DeleteUserEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Delete("/api/deleteUser/{UserId}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
        => await SendAsync(new DeleteUserResponse(await _userService.DeleteUser(req.UserId)), cancellation: ct);
}