using System.Net;
using Infrastructure.Data.Shared;

namespace Application.Features.ClientFeatures.Command;

public class CreateNewClientException : BarberShopException
{
    public CreateNewClientException(string message) : base(message) { }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}