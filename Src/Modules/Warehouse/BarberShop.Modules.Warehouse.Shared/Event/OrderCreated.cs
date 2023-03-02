namespace BarberShop.Modules.Warehouse.Shared.Event;

public sealed record OrderCreated(Guid OrderId, DateTime DeliveryTime);

