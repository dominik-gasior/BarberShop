using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Command;

internal sealed record CreateNewUserRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string NumberPhone { get; init; }
    public string? Email { get; init; }
    public Role Role { get; set; }
}

internal sealed class CreateNewUserMapperProfile : Mapper<CreateNewUserRequest, Guid, User>, IRequestMapper
{
    public override User ToEntity(CreateNewUserRequest r) => new User
    {
        NumberPhone = r.NumberPhone,
        FirstName = r.FirstName,
        LastName = r.LastName,
        Email = r.Email,
        Role = r.Role
    };

    public override Guid FromEntity(User e)
        => e.Id;
}
internal sealed class CreateNewUserEndpoint : EndpointWithMapper<CreateNewUserRequest, CreateNewUserMapperProfile>
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