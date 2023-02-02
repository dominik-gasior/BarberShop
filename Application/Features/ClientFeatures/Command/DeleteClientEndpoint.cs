using Application.ServiceManager;
using FastEndpoints;

namespace Application.Features.ClientFeatures.Command;

public record DeleteClientRequest { public int Id { get; init; } }
public record struct DeleteClientResponse(string Message);

public class DeleteClientEndpoint : Endpoint<DeleteClientRequest, DeleteClientResponse>
{
    private readonly IServiceManager _serviceManager;
    public DeleteClientEndpoint(IServiceManager serviceManager) => _serviceManager = serviceManager;

    public override void Configure()
    {
        Delete("/api/deleteClient/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteClientRequest req, CancellationToken ct)
        => await SendAsync(new DeleteClientResponse(await _serviceManager.ClientService.DeleteClient(req.Id, ct)), cancellation: ct);
}