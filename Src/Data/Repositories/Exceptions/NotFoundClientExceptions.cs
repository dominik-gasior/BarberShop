using System.Net;
using Src.Data.Shared;

namespace Src.Features.ClientFeatures.Exceptions;

public class NotFoundExceptions : BarberShopException
{
    public NotFoundExceptions(string message) : base(message) { }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}