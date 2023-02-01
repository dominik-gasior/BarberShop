using Application.ServiceManager;
using FastEndpoints;
using Src.Domain;

namespace Application.Features.ClientFeatures.Query;

public record struct GetAllClientsResponse(int Id, string FirstName, string LastName, string Email, string NumberPhone);
public record struct GetAllClientsRequest;
public class GetAllClientsMapperProfile : Mapper<GetAllClientsRequest, IEnumerable<GetAllClientsResponse>, IEnumerable<Client>>, IResponseMapper
{
    public override IEnumerable<GetAllClientsResponse> FromEntity(IEnumerable<Client> c)
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
    private readonly IServiceManager _serviceManager;
    public GetAllClientsEndpoint(IServiceManager serviceManager) => _serviceManager = serviceManager;
    public override void Configure()
    {
        Get("/api/getAllClients");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _serviceManager.ClientService.GetAllClients(ct)), cancellation: ct);
}