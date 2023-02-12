using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Users.Api.Exceptions;

internal sealed class UserNotFoundByNumberPhoneException : BarberShopExceptions
{
    public UserNotFoundByNumberPhoneException(string numberPhone) : base($"User with number phone {numberPhone} not found!"){}

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}