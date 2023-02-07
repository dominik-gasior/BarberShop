using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Query;

public record GetUserByNumberPhoneRequest
{
    public string NumberPhone { get; set; }   
}

public record GetUserByNumberPhoneResponse(int Id, string FirstName, string LastName, string NumberPhone,
    string Email);

public class GetUserByNumberPhoneMapperProfile : Mapper<GetUserByNumberPhoneRequest, GetUserByNumberPhoneResponse, User>
{
    public override GetUserByNumberPhoneResponse FromEntity(User e)
        => new GetUserByNumberPhoneResponse
        (
            e.Id,
            e.FirstName,
            e.LastName,
            e.NumberPhone,
            e.Email
        );
}
public class GetUserByNumberPhoneEndpoint : Endpoint<GetUserByNumberPhoneRequest, GetUserByNumberPhoneResponse, GetUserByNumberPhoneMapperProfile>
{
    private readonly IUserService _userService;

    public GetUserByNumberPhoneEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Get("/api/user/numberPhone/{numberPhone}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(GetUserByNumberPhoneRequest req, CancellationToken ct)
        => await SendAsync(
            Map.FromEntity(await _userService.GetUserByNumberPhone(req.NumberPhone, ct)), cancellation: ct);
}