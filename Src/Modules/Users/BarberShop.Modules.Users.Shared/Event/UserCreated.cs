namespace BarberShop.Modules.Users.Shared.Event;

public sealed record UserCreated(Guid Id, string Fullname, string Email, string NumberPhone);