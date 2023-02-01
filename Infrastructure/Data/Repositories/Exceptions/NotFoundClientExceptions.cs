using System.Net;
using Infrastructure.Data.Shared;

namespace Infrastructure.Data.Repositories.Exceptions;

internal class NotFoundExceptions : BarberShopException
{
    public NotFoundExceptions(string message) : base(message) { }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}