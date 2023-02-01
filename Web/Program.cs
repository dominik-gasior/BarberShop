global using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Src.Manager.RepositoryManager;
using Src.Manager.ServiceManager;

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