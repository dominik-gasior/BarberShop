using System.Net;
using Src.Data.Shared;

namespace Src.Features.ClientFeatures.Exceptions;

public class BadRequestException : BarberShopException
{
    public BadRequestException(string message) : base(message){}
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
