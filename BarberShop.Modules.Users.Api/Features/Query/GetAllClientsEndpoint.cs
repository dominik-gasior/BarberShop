using BarberShop.Modules.Users.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Users.Api.Features.Query;

public record struct GetAllClientsResponse(int Id, string FirstName, string LastName, string Email, string NumberPhone);
public record struct GetAllClientsRequest;
public class GetAllClientsMapperProfile : Mapper<GetAllClientsRequest, IEnumerable<GetAllClientsResponse>, IEnumerable<User>>, IResponseMapper
{
    public override IEnumerable<GetAllClientsResponse> FromEntity(IEnumerable<User> c)
        => c.Select(r => new GetAllClientsResponse
        {
            Id = r.Id,
            FirstName = r.FirstName,
            LastName = r.LastName,
            Email = r.Email,
            NumberPhone = r.NumberPhone
        });
}
public class GetAllClientsEndpoint : EndpointWithoutRequest<IEnumerable<GetAllClientsResponse>,GetAllClientsMapperProfile>
{
    private readonly IUserService _userService;
    public GetAllClientsEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Get("/api/getAllClients");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _userService.GetAllUsers(ct)), cancellation: ct);
}