using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Users.Api.Exceptions;

internal sealed class NumberPhoneisExistException : BarberShopExceptions
{
    public NumberPhoneisExistException(string numberPhone) : base($"Number phone {numberPhone} is exist in database!") { }
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}