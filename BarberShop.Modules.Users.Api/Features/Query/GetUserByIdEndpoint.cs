using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Query;

public record GetUserByIdRequest
{
    public int Id { get;}
}
public record GetUserByIdResponse(int Id, string FirstName, string LastName, string NumberPhone, string Email);

public class GetUserByIdMapperProfile : Mapper<GetUserByIdRequest, GetUserByIdResponse, User>
{
    public override GetUserByIdResponse FromEntity(User e)
        => new GetUserByIdResponse(e.Id, e.FirstName, e.LastName, e.NumberPhone, e.Email);
}
public class GetUserByIdEndpoint : Endpoint<GetUserByIdRequest, GetUserByIdResponse, GetUserByIdMapperProfile>
{
    private readonly IUserService _userService;

    public GetUserByIdEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Get("/api/user/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserByIdRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _userService.GetUserById(req.Id, ct)), cancellation: ct);
}