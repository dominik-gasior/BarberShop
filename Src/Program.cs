using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Src.Data;
using Src.Features.Client.Query;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();
var connString = builder.Configuration.GetConnectionString("dbConnString");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connString)
);
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
Seeder.Seed(dbContext);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/api/clients", async (IMediator mediator)
=> Results.Ok(mediator.Send(new GetAllClientQuery())));
// app.MapControllers();

app.Run();

