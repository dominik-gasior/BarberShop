global using FastEndpoints;
using BarberShop.Modules.SystemReservation.Api;
using BarberShop.Modules.Users.Api;
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

app.UseAuthorization();

app.UseFastEndpoints();

app.UseSwaggerGen();

app.Run();