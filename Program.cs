using MappingApp.Interfaces;
using MappingApp.Models;
using MappingApp.Repository;
using MappingApp.Repository.Point;
using MappingApp.Services;
using MappingApp.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL Baðlantýsý
builder.Services.AddDbContext<MappingAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection Kayýtlarý
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPointRepository, PointRepository>();
builder.Services.AddScoped<IPointService, EFPointService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


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
app.UseCors("AllowSpecificOrigins");

app.Run();
