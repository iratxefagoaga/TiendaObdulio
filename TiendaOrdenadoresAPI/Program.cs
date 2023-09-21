using Microsoft.EntityFrameworkCore;
using NLog;
using TiendaOrdenadoresAPI.Data;
using TiendaOrdenadoresAPI.Logging;
using TiendaOrdenadoresAPI.Services.FakeRepositories;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;
using TiendaOrdenadoresAPI.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);
var path = Directory.GetCurrentDirectory();

builder.Services.AddDbContext<OrdenadoresContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrdenadoresContext")
        ?.Replace("[DataDirectory]", path)));
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

builder.Services.AddScoped<IGenericRepositoryAdo<Componente>, ComponenteAdoRepository>();
builder.Services.AddScoped<IGenericRepositoryAdo<Ordenador>, OrdenadoresAdoRepository>();
builder.Services.AddScoped<IGenericRepositoryAdo<Cliente>, ClienteAdoRepository>();
builder.Services.AddScoped<IGenericRepositoryAdo<Pedido>, PedidoAdoRepositroy>();
builder.Services.AddScoped<IGenericRepositoryAdo<Factura>, FacturasAdoRepository>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

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
