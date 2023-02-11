global using FastEndpoints;
using System.Reflection;
using BarberShop.Modules.SystemReservation.Api;
using BarberShop.Modules.SystemReservation.Api.Consumers;
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
    var entryAssembly = Assembly.GetEntryAssembly();
    x.AddConsumer<UserConsumer>();
    x.AddActivities(entryAssembly);
    
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