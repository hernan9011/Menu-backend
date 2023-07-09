using Application.Interface;
using Application.Interface.Command;
using Application.Interface.Query;
using Application.UseCase;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Query;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Servicios de controladores y CORS
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{   options.AddPolicy("CorsPolicy", builder =>
    {   builder
            .WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Documentación de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de la base de datos
var cs = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(cs));

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Registro de servicios
builder.Services.AddTransient<IMercaderiaQuery, MercaderiaQuery>();
builder.Services.AddTransient<IMercaderiaCommand, MercaderiaCommand>();
builder.Services.AddTransient<IServicesMercaderia, ServicesMercaderia>();

builder.Services.AddTransient<IComandaQuery, ComandaQuery>();
builder.Services.AddTransient<IComandaCommand, ComandaCommand>();
builder.Services.AddTransient<IServicesComanda, ServicesComanda>();

builder.Services.AddTransient<IComandaMercaderiaQuery, ComandaMercaderiaQuery>();
builder.Services.AddTransient<IComandaMercaderiaCommand, ComandaMercaderiaCommand>();

var app = builder.Build();

// Configuraciones adicionales para el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware y enrutamiento
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
