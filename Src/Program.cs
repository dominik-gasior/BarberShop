using Microsoft.EntityFrameworkCore;
using Src.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var connString = builder.Configuration.GetConnectionString("dbConnString");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connString)
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();