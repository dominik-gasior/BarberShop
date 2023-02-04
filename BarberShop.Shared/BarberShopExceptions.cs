using System.Net;

namespace BarberShop.Shared;

public abstract class BarberShopExceptions : Exception
{
    public abstract HttpStatusCode StatusCode { get; }

    protected BarberShopExceptions(string message) : base(message){}
}