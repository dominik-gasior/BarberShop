using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Users.Api.Exceptions;

internal class UserNotFoundByIdException : BarberShopExceptions
{
    public int Id { get;}

    public UserNotFoundByIdException(int id) : base($"User with id {id} not found!")
        => Id = id;

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}