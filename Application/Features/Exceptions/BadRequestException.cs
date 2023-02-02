using System.Net;
using Infrastructure.Data.Shared;

namespace Application.Features.Exceptions;

internal class BadRequestException : BarberShopException
{
    public BadRequestException(string message) : base(message){}
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
