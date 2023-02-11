namespace BarberShop.Modules.Users.Shared.Event;

public sealed record UserCreated(int Id, string Fullname, string Email, string NumberPhone);