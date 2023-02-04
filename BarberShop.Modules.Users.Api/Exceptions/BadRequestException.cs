using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Users.Api.Exceptions;

internal class BadRequestException : BarberShopExceptions
{
    public BadRequestException(string message) : base(message){}
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
