using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebCRUDAPI.Datos;
using WebCRUDAPI.Services;
using WebCRUDAPI.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IFechaServices, FechasServices>();
builder.Services.AddScoped<IZonasServices, ZonasServices>();
builder.Services.AddScoped<IImagenService, ImagenServices>();

builder.Services.AddDbContext<lockersqaContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("defaultConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.30-mysql")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
