using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.Users.Api.Exceptions;

internal sealed class UserIsExistException : BarberShopExceptions
{
    public int Id { get;}

    public UserIsExistException(int id) : base($"User id with {id} is exist!")
        => Id = id;
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
