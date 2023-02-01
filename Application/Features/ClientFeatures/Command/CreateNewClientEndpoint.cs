using Application.Features.ClientFeatures.Query;
using FastEndpoints;
using Src.Domain;
using Src.Manager.ServiceManager;

namespace Application.Features.ClientFeatures.Command;

public record CreateNewClientRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string NumberPhone { get; init; }
    public string? Email { get; init; }
}
public record struct CreateNewClientResponse(int Id);
public class CreateNewClientMapperProfile : Mapper<CreateNewClientRequest, CreateNewClientResponse, Client>
{
    public override CreateNewClientResponse FromEntity(Client e)
        => new CreateNewClientResponse
        {
            Id = e.Id
        };

    public override Client ToEntity(CreateNewClientRequest r)
        => new Client
        {
            FirstName = r.FirstName,
            LastName = r.LastName,
            Email = r.Email,
            NumberPhone = r.NumberPhone,
        };
}

public class CreateNewClientEndpoint : Endpoint<CreateNewClientRequest,CreateNewClientResponse, CreateNewClientMapperProfile>
{
    private readonly IServiceManager _serviceManager;

    public CreateNewClientEndpoint(IServiceManager serviceManager) => _serviceManager = serviceManager;

    public override void Configure()
    {
        Post("/api/client");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateNewClientRequest req, CancellationToken ct)
    {
        // var result = await _serviceManager.ClientService.CreateNewClient(Map.ToEntity(req), ct);

        // await SendAsync(Map.FromEntity(result), cancellation:ct);
    }
}