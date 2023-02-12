using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.SystemReservation.Api.Exceptions;

internal sealed class NotFoundVisitByIdException : BarberShopExceptions
{
    public NotFoundVisitByIdException(Guid id) : base($"Visit id with {id} not found!"){}
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}