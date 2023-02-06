using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;
using Microsoft.Identity.Client;

namespace BarberShop.Modules.Users.Api.Features.Query;

public record GetUserByIdRequest { public int Id { get; init; } }
public record struct GetUserByIdResponse(int Id, string FirstName, string LastName, string NumberPhone, string Email);

public class GetUserByIdMapperProfile : Mapper<GetUserByIdRequest, GetUserByIdResponse, User>
{
    public override GetUserByIdResponse FromEntity(User e)
        => new()
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            NumberPhone = e.NumberPhone,
        };
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