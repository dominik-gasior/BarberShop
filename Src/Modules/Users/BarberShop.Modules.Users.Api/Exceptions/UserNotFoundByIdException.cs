using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Users.Api.Exceptions;

internal sealed class UserNotFoundByIdException : BarberShopExceptions
{
    public UserNotFoundByIdException(Guid id) : base($"User with id {id} not found!"){}

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}