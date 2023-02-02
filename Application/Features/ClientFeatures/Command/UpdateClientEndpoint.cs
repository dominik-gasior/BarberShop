using Application.ServiceManager;
using FastEndpoints;
using Infrastructure.Domain;

namespace Application.Features.ClientFeatures.Command;

public record UpdateClientRequest { public int Id { get; init; } public string? Email { get; init; } public string? NumberPhone { get; init; } }
public record UpdateClientResponse(string Message);

public class UpdateClientMapperProfile : Mapper<UpdateClientRequest, UpdateClientResponse, Client>
{
    
    public override Client ToEntity(UpdateClientRequest r)
        => new Client
        {
            Id = r.Id,
            NumberPhone = r.NumberPhone,
            Email = r.Email
        };
}
public class UpdateClientEndpoint : Endpoint<UpdateClientRequest, UpdateClientResponse, UpdateClientMapperProfile>
{
    private readonly IServiceManager _serviceManager;
    public UpdateClientEndpoint(IServiceManager serviceManager) => _serviceManager = serviceManager;
    
    public override void Configure()
    {
        Put("/api/updateClient");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateClientRequest req, CancellationToken ct)
        => await SendAsync(new UpdateClientResponse(await _serviceManager.ClientService.UpdateClient(Map.ToEntity(req), ct)), cancellation: ct);
}