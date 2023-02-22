using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Warehouse.Api.Exceptions.Orders;

internal sealed class NotFoundOrderByIdException : BarberShopExceptions
{
    public NotFoundOrderByIdException(Guid id) : base($"Order id with {id} not found!"){}

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}