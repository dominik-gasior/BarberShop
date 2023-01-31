using MediatR;

namespace Src.Features.Client.Query;

public class GetAllClientQuery : IRequest<List<Domain.Client>> {}

public class GetAllClientQueryHandler : IRequestHandler<GetAllClientQuery, List<Domain.Client>>
{
    private readonly IClientRepository _clientRepository;
    public GetAllClientQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public Task<List<Domain.Client>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
        => _clientRepository.GetAllClients(cancellationToken);

}