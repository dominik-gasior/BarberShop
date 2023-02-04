global using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

var connString = builder.Configuration.GetConnectionString("dbConnString");

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

var app = builder.Build();

app.UseAuthorization();


app.UseFastEndpoints();

app.UseSwaggerGen();

app.Run();