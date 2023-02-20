using BarberShop.Modules.SystemReservation.Api.Entities;
using FastEndpoints;
using FluentValidation;

namespace BarberShop.Modules.SystemReservation.Api.Features.Command;

internal sealed record CreateVisitRequest
{
    public string NumberPhone { get; set; }
    public Guid UserId { get; set; }
    public Guid EmployeeId { get; set; }
    public int ServiceIndustryId { get; set; }
    public DateTime Date { get; set; }
}

internal sealed class CreateVisitMapperProfile : Mapper<CreateVisitRequest, Guid, Visit>, IRequestMapper
{
    public override Visit ToEntity(CreateVisitRequest r)
        => new Visit
        {
            EmployeeGuid = r.EmployeeId,
            ClientGuid = r.UserId,
            ServiceIndustryId = r.ServiceIndustryId,
            Date = r.Date,
        };

    public override Guid FromEntity(Visit e)
        => e.Id;
}

internal sealed class CreateVisitEndpoint : EndpointWithMapper<CreateVisitRequest,CreateVisitMapperProfile>
{
    private readonly ISystemReservationService _systemReservationService;
    public CreateVisitEndpoint(ISystemReservationService systemReservationService) =>
        _systemReservationService = systemReservationService;

    public override void Configure()
    {
        Post("/api/createVisit");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateVisitRequest req, CancellationToken ct)
        => await _systemReservationService.CreateNewVisit(Map.ToEntity(req));
}

internal sealed class CreateVisitValidator : Validator<CreateVisitRequest>
{
    public CreateVisitValidator()
    {
        RuleFor(x => x.NumberPhone)
            .NotEmpty()
            .WithMessage("Number phone is required!")
            .Must(number => number.Length == 9)
            .WithMessage("Number phone should be 9 characters!");

    }
}