global using FastEndpoints;
using BarberShop.Modules.Notifications.Api.Customers;
using BarberShop.Modules.SystemReservation.Api;
using BarberShop.Modules.Users.Api;
using BarberShop.Modules.Warehouse.Api;
using FastEndpoints.Swagger;
using MassTransit;

var builder = WebApplication.CreateBuilder();

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddWarehouseModule(builder.Configuration);
builder.Services.AddSystemReservationModule(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    
    x.AddConsumers(typeof(SystemReservationExtensions).Assembly);
    x.AddConsumers(typeof(WarehouseExtensions).Assembly);
    x.AddConsumers(typeof(INotificationConsumer).Assembly);
    
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
app.UseWarehouseModule();

app.UseDefaultExceptionHandler();
app.UseAuthorization();

app.UseFastEndpoints();

app.UseSwaggerGen();

app.Run();
