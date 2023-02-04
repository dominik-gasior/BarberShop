using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Users.Api.Exceptions;

internal class NotFoundExceptions : BarberShopExceptions
{
    public NotFoundExceptions(string message) : base(message) { }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}