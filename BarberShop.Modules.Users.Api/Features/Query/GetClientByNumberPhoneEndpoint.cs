using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Query;

public record GetClientByNumberPhoneRequest{public string numberPhone { get; init; }}

public record struct GetClientByNumberPhoneResponse(int Id, string FirstName, string LastName, string NumberPhone,
    string Email);

public class
    GetClientByNumberPhoneMapperProfile : Mapper<GetClientByNumberPhoneRequest, GetClientByNumberPhoneResponse, User>
{
    public override GetClientByNumberPhoneResponse FromEntity(User e)
        => new()
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            NumberPhone = e.NumberPhone,
        };
}
public class GetClientByNumberPhoneEndpoint : Endpoint<GetClientByNumberPhoneRequest, GetClientByNumberPhoneResponse, GetClientByNumberPhoneMapperProfile>
{
    private readonly IUserService _userService;

    public GetClientByNumberPhoneEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Get("/api/client/numberPhone/{numberPhone}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(GetClientByNumberPhoneRequest req, CancellationToken ct)
        => await SendAsync(
            Map.FromEntity(await _userService.GetUserByNumberPhone(req.numberPhone, ct)), cancellation: ct);
}