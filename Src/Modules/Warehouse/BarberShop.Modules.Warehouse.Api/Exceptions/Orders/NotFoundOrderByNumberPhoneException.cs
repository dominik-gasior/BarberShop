using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Warehouse.Api.Exceptions.Orders;

internal sealed class NotFoundOrderByNumberPhoneException : BarberShopExceptions
{
    public NotFoundOrderByNumberPhoneException(string numberPhone) : base($"Order id with {numberPhone} not found!"){}

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}