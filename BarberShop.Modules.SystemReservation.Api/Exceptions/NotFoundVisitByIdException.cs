using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.SystemReservation.Api.Exceptions;

internal class NotFoundVisitByIdException : BarberShopExceptions
{
    public int Id { get; }

    public NotFoundVisitByIdException(int id) : base($"Visit id with {id} not found!")
        => Id = id;

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}