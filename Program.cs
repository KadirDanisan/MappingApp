using MappingApp.Interfaces;
using MappingApp.Models;
using MappingApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL Ba�lant�s�
builder.Services.AddDbContext<MappingAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection Kay�tlar�
builder.Services.AddScoped<IPointService, PointService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mapping API V1");
    });
}

app.UseRouting();
app.MapControllers();

app.Run();
