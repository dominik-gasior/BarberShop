using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;
using Microsoft.Identity.Client;

namespace BarberShop.Modules.Users.Api.Features.Query;

public record GetClientByIdRequest { public int Id { get; init; } }
public record struct GetClientByIdResponse(int Id, string FirstName, string LastName, string NumberPhone, string Email);

public class GetClientByIdMapperProfile : Mapper<GetClientByIdRequest, GetClientByIdResponse, User>
{
    public override GetClientByIdResponse FromEntity(User e)
        => new()
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            NumberPhone = e.NumberPhone,
        };
}
public class GetClientByIdEndpoint : Endpoint<GetClientByIdRequest, GetClientByIdResponse, GetClientByIdMapperProfile>
{
    private readonly IUserService _userService;

    public GetClientByIdEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Get("/api/client/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetClientByIdRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _userService.GetUserById(req.Id, ct)), cancellation: ct);
}