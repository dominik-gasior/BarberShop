using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Users.Api.Exceptions;

internal sealed class UserNotFoundByNumberPhoneException : BarberShopExceptions
{
    public string NumberPhone { get;}

    public UserNotFoundByNumberPhoneException(string numberPhone) : base($"User with number phone {numberPhone} not found!")
        => NumberPhone = numberPhone;

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}