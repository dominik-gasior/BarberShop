namespace BarberShop.Modules.Users.Shared.Event;

public sealed record UserUpdated(Guid Id, string NumberPhone, string Email, bool IsClient = true);
