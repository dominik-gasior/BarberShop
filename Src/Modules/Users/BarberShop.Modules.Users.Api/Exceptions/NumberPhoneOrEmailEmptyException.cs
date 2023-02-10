using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Users.Api.Exceptions;

internal sealed class NumberPhoneOrEmailEmptyException : BarberShopExceptions
{
    public NumberPhoneOrEmailEmptyException() : base("Email or number phone are empty"){}
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}