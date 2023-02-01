using System.Formats.Asn1;
using Application.Features.ClientFeatures.Command;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories.Exceptions;
using Src.Domain;

namespace Application.Features.ClientFeatures;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllClients(CancellationToken ct);
    Task<Client> GetClientById(int id, CancellationToken ct);
    Task<Client> GetClientByNumberPhone(string numberPhone, CancellationToken ct);
    Task<Client> CreateNewClient(Client client, CancellationToken ct);
    Task<string> DeleteClient(int id, CancellationToken ct);
    Task<string> UpdateClient(Client client, CancellationToken ct);
}
internal class ClientService : IClientService
{
    private readonly IRepositoryManager _repositoryManager;

    public ClientService(IRepositoryManager repositoryManager)
        => _repositoryManager = repositoryManager;

    public async Task<IEnumerable<Client>> GetAllClients(CancellationToken ct)
        => await _repositoryManager.ClientRepository.GetAllClients(ct);
    
    public async Task<Client> GetClientById(int id, CancellationToken ct)
    {
        var client = await _repositoryManager.ClientRepository.GetClientById(id, ct);
        if (client is null) throw new NotFoundExceptions("Not found client in database");
        return client;
    }
    public async Task<Client> GetClientByNumberPhone(string numberPhone, CancellationToken ct)
    {
        var client = await _repositoryManager.ClientRepository.GetClientByNumberPhone(numberPhone, ct);
        if (client is null) throw new NotFoundExceptions("Not found client in database");
        return client;
    }

    public async Task<Client> CreateNewClient(Client client, CancellationToken ct)
    {
        var isClient = await _repositoryManager.ClientRepository.GetClientByNumberPhone(client.NumberPhone, ct, false);
        if (isClient is not null) throw new BadRequestException($"Client #{client.Id} is exist in database");

        await _repositoryManager.ClientRepository.Insert(client,ct);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
        return await _repositoryManager.ClientRepository.GetClientByNumberPhone(client.NumberPhone,ct);
    }

    public async Task<string> DeleteClient(int id, CancellationToken ct)
    {
        var client = await _repositoryManager.ClientRepository.GetClientById(id, ct, false);
        
        if (client is null) throw new BadRequestException("Not found client in database");

        await _repositoryManager.ClientRepository.Delete(client);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        return $"Client #{id} was removed in database!";
    }

    public async Task<string> UpdateClient(Client client, CancellationToken ct)
    {
        if (client.NumberPhone.Equals("") && client.Email!.Equals("")) throw new BadRequestException("Email or number phone are empty!");
        
        var updateClient = await _repositoryManager.ClientRepository.GetClientById(client.Id, ct, false);
        if (updateClient is null) throw new NotFoundExceptions("Not found client in database");

        if(!client.NumberPhone.Equals("")) updateClient.NumberPhone = client.NumberPhone;
        if(!client.Email!.Equals("")) updateClient.Email = client.Email;
        
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
        return $"Client #{client.Id} was updated in database";
    }
}