namespace BarberShop.Modules.Warehouse.Shared.Event;

public sealed record OrderStatusChanged(Guid OrderId, string OrderStatus);