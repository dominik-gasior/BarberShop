using System.Net;
using BarberShop.Shared;

namespace BarberShop.Modules.SystemReservation.Api.Exceptions;

public class BusyVisitException : BarberShopExceptions
{
    
    public BusyVisitException() : base("Visit is busy in database!") { }
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}