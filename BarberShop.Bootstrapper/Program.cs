global using FastEndpoints;
using BarberShop.Modules.SystemReservation.Api;
using BarberShop.Modules.Users.Api;
using BarberShop.Modules.Warehouse.Api;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder();

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();
builder.Services.AddUsersModule();
builder.Services.AddWarehouseModule();
builder.Services.AddSystemReservationModule();

var app = builder.Build();

app.UseSystemReservationModule();

app.UseAuthorization();

app.UseFastEndpoints();

app.UseSwaggerGen();

app.Run();