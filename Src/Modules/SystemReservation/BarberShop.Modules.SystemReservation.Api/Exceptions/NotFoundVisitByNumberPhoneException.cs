using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.SystemReservation.Api.Exceptions;

public class NotFoundVisitByNumberPhoneException : BarberShopExceptions
{
    public string NumberPhone { get; }

    public NotFoundVisitByNumberPhoneException(string numberPhone) : base($"Visit id with {numberPhone} not found!")
        => NumberPhone = numberPhone;

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}