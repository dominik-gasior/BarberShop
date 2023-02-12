namespace BarberShop.Modules.Users.Shared.Event;

public sealed record UserDeleted(Guid Id, bool IsClient = true);