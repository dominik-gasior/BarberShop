using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Warehouse.Api.Exceptions.Products;

internal sealed class NotFoundProductByIdException : BarberShopExceptions
{
    public NotFoundProductByIdException(int id) : base($"Product id with {id} not found!"){}

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}