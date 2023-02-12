using FastEndpoints;
using FluentValidation;

namespace BarberShop.Modules.Users.Api.Features.Command;

internal sealed record UpdateUserRequest { public Guid Id { get; init; } public string? Email { get; init; } public string? NumberPhone { get; init; } }

internal sealed record UpdateUserResponse(string Message);
internal sealed class UpdateUserEndpoint : Endpoint<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IUserService _userService;
    public UpdateUserEndpoint(IUserService userService) => _userService = userService;
    public override void Configure()
    {
        Put("/api/updateUser");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
        => await SendAsync(new UpdateUserResponse(await _userService.UpdateUser(req)), cancellation: ct);
}

internal sealed class UpdateUserValidator : Validator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.NumberPhone)
            .Must(number => number.Length is 9 or 0)
            .WithMessage("Number phone should be 9 characters!");
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("E-mail is not correct!");
    }
}