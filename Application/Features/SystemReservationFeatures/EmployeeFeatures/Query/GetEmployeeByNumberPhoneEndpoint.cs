using Application.ServiceManager;
using FastEndpoints;
using Infrastructure.Domain.SystemReservation;

namespace Application.Features.SystemReservationFeatures.EmployeeFeatures.Query;

public record GetEmployeeByNumberPhoneRequest{ public string NumberPhone { get; init; }}

public record struct GetEmployeeByNumberPhoneResponse(int Id, string FirstName, string LastName, string Email,
    string NumberPhone, string NameRole, IEnumerable<Visit> Visits);

public class GetEmployeeByNumberPhoneMapperProfile : Mapper<GetEmployeeByNumberPhoneRequest, GetEmployeeByNumberPhoneResponse, Employee>
{
    public override GetEmployeeByNumberPhoneResponse FromEntity(Employee e)
        => new GetEmployeeByNumberPhoneResponse
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

public class GetEmployeeByNumberPhoneEndpoint : Endpoint<GetEmployeeByNumberPhoneRequest, GetEmployeeByNumberPhoneResponse, GetEmployeeByNumberPhoneMapperProfile>
{
    private readonly IServiceManager _serviceManager;

    public GetEmployeeByNumberPhoneEndpoint(IServiceManager serviceManager) => _serviceManager = serviceManager;
    public override void Configure()
    {
        Get("/api/getEmployeeByNumberPhone/{numberPhone}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetEmployeeByNumberPhoneRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _serviceManager.EmployeeService.GetEmployeeByNumberPhone(req.NumberPhone, ct)), cancellation: ct);
}