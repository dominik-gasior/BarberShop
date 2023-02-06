using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Query;

public record struct GetAllUsersResponse(int Id, string FirstName, string LastName, string Email, string NumberPhone);
public record struct GetAllUsersRequest;
public class GetAllUsersMapperProfile : Mapper<GetAllUsersRequest, IEnumerable<GetAllUsersResponse>, IEnumerable<User>>, IResponseMapper
{
    public override IEnumerable<GetAllUsersResponse> FromEntity(IEnumerable<User> c)
        => c.Select(r => new GetAllUsersResponse
        {
            Id = r.Id,
            FirstName = r.FirstName,
            LastName = r.LastName,
            Email = r.Email,
            NumberPhone = r.NumberPhone
        });
}
public class GetAllUsersEndpoint : EndpointWithoutRequest<IEnumerable<GetAllUsersResponse>,GetAllUsersMapperProfile>
{
    private readonly IUserService _userService;
    public GetAllUsersEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Get("/api/getAllUsers");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _userService.GetAllUsers(ct)), cancellation: ct);
}