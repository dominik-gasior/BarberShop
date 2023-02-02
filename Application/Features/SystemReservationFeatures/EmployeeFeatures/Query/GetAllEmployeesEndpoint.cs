using Application.ServiceManager;
using FastEndpoints;
using Infrastructure.Domain.SystemReservation;

namespace Application.Features.SystemReservationFeatures.EmployeeFeatures.Query;

public record struct GetAllEmployeesResponse(int Id, string FirstName, string LastName, string Email, string NumberPhone, string RoleName);
public class GetAllEmployeesMapperProfile : ResponseMapper<IEnumerable<GetAllEmployeesResponse>, IEnumerable<Employee>>
{
    public override IEnumerable<GetAllEmployeesResponse> FromEntity(IEnumerable<Employee> e)
        => e.Select(c => new GetAllEmployeesResponse
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            NumberPhone = c.NumberPhone,
            RoleName = c.Role.Name,
            Email = c.Email
        });
}
public class GetAllEmployeesEndpoint : EndpointWithoutRequest<IEnumerable<GetAllEmployeesResponse>, GetAllEmployeesMapperProfile>
{
    private readonly IServiceManager _serviceManager;
    public GetAllEmployeesEndpoint(IServiceManager serviceManager) => _serviceManager = serviceManager;

    public override void Configure()
    {
        Get("/api/getAllEmployees");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _serviceManager.EmployeeService.GetAllEmployees(ct)), cancellation: ct);
}