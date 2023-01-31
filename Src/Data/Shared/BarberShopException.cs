using System.Net;

namespace Src.Data.Shared;

public abstract class BarberShopException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }

    protected BarberShopException(string message) : base(message){}
}