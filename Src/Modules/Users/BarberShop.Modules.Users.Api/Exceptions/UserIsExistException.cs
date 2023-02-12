using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Users.Api.Exceptions;

internal sealed class UserIsExistException : BarberShopExceptions
{
    public UserIsExistException(string numberPhone) : base($"User with {numberPhone} is exist!"){}
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
