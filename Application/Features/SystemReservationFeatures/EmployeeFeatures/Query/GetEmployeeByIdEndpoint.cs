using Application.Features.ClientFeatures;
using Application.ServiceManager;
using FastEndpoints;
using Infrastructure.Domain.SystemReservation;

namespace Application.Features.SystemReservationFeatures.EmployeeFeatures.Query;

public record GetEmployeeByIdRequest{ public int Id { get; init; }}

public record struct GetEmployeeByIdResponse(int Id, string FirstName, string LastName, string Email,
    string NumberPhone, string NameRole, IEnumerable<Visit> Visits);

public class GetEmployeeByIdMapperProfile : Mapper<GetEmployeeByIdRequest, GetEmployeeByIdResponse, Employee>
{
    public override GetEmployeeByIdResponse FromEntity(Employee e)
        => new GetEmployeeByIdResponse
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            NumberPhone = e.NumberPhone,
            Visits = e.Visits,
            NameRole = e.Role.Name
        };
}

public class GetEmployeeByIdEndpoint : Endpoint<GetEmployeeByIdRequest, GetEmployeeByIdResponse, GetEmployeeByIdMapperProfile>
{
    private readonly IServiceManager _serviceManager;

    public GetEmployeeByIdEndpoint(IServiceManager serviceManager) => _serviceManager = serviceManager;
    public override void Configure()
    {
        Get("/api/getEmployeeById/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetEmployeeByIdRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _serviceManager.EmployeeService.GetEmployeeById(req.Id, ct)), cancellation: ct);
    
}