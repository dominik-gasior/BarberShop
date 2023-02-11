using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Users.Api.Exceptions;

internal sealed class UserIsExistException : BarberShopExceptions
{
    public string NumberPhone { get;}

    public UserIsExistException(string numberPhone) : base($"User with {numberPhone} is exist!")
        => NumberPhone = numberPhone;
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
