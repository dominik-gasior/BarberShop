global using FastEndpoints;
using Application.ServiceManager;
using FastEndpoints.Swagger;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

var connString = builder.Configuration.GetConnectionString("dbConnString");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connString)
);
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
Seeder.Seed(dbContext);

app.UseAuthorization();


app.UseFastEndpoints();

app.UseSwaggerGen();

app.Run();