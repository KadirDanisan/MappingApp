using MappingApp.Interfaces;
using MappingApp.Services;
using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// POSTGRESQL
builder.Services.AddScoped<IPointService>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new PointInsertService(connectionString);
});


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