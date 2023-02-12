using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.SystemReservation.Api.Exceptions;

internal sealed class NotFoundVisitByNumberPhoneException : BarberShopExceptions
{
    public NotFoundVisitByNumberPhoneException(string numberPhone) : base($"Visit id with {numberPhone} not found!"){}

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}