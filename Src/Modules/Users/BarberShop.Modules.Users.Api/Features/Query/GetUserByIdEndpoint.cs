using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Query;

internal sealed record GetUserByIdRequest { public Guid UserId { get;} }
internal sealed record GetUserByIdResponse(Guid Id, string FirstName, string LastName, string NumberPhone, string Email);

internal sealed class GetUserByIdMapperProfile : Mapper<GetUserByIdRequest, GetUserByIdResponse, User>
{
    public override GetUserByIdResponse FromEntity(User e)
        => new GetUserByIdResponse(e.Id, e.FirstName, e.LastName, e.NumberPhone, e.Email);
}
internal sealed class GetUserByIdEndpoint : Endpoint<GetUserByIdRequest, GetUserByIdResponse, GetUserByIdMapperProfile>
{
    private readonly IUserService _userService;

    public GetUserByIdEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Get("/api/user/{UserId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserByIdRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _userService.GetUserById(req.UserId)), cancellation: ct);
}