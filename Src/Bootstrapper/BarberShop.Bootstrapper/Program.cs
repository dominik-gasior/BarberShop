global using FastEndpoints;
using BarberShop.Modules.Notifications.Api.Customers;
using BarberShop.Modules.SystemReservation.Api;
using BarberShop.Modules.Users.Api;
using BarberShop.Modules.Users.Shared.Event;
using BarberShop.Modules.Warehouse.Api;
using FastEndpoints.Swagger;
using MassTransit;

var builder = WebApplication.CreateBuilder();

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();
builder.Services.AddUsersModule();
builder.Services.AddWarehouseModule();
builder.Services.AddSystemReservationModule();

builder.Services.AddMassTransit(x =>
{
    
    x.AddConsumers(typeof(SystemReservationExtensions).Assembly);
    //todo refactor consumer
    x.AddConsumer<UserCustomer>();
    x.AddConsumer<VisitCustomer>();
    x.UsingRabbitMq((context,cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseSystemReservationModule();
app.UseDefaultExceptionHandler();
app.UseAuthorization();

app.UseFastEndpoints();

app.UseSwaggerGen();

app.Run();