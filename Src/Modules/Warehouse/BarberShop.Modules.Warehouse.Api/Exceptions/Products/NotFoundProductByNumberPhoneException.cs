using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Warehouse.Api.Exceptions.Products;

internal sealed class NotFoundProductByNumberPhoneException : BarberShopExceptions
{
    public NotFoundProductByNumberPhoneException(string numberPhone) : base($"Product id with {numberPhone} not found!"){}

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}