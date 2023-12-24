using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.ServicioWeb.Servicios;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebServer.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

string strConnection = builder.Configuration.GetSection("connectionString").Value;

builder.Services.AddDbContext<LeandroContext>(x => x.UseMySQL(strConnection), ServiceLifetime.Transient);
builder.Services.AddSingleton<IConfiguracionGeneral, ConfiguracionGeneral>();
builder.Services.AddSingleton<IConfiguracionRecepcion, ConfiguracionRecepcion>();
builder.Services.AddSingleton<IMantenimientoService, MantenimientoService>();
builder.Services.AddSingleton<IFacturacionService, FacturacionService>();
builder.Services.AddSingleton<ICompraService, CompraService>();
builder.Services.AddSingleton<IFlujoCajaService, FlujoCajaService>();
builder.Services.AddSingleton<IBancaService, BancaService>();
builder.Services.AddSingleton<IReporteService, ReporteService>();
builder.Services.AddSingleton<IContabilidadService, ContabilidadService>();
builder.Services.AddSingleton<ITrasladoService, TrasladoService>();
builder.Services.AddSingleton<ICuentaPorProcesarService, CuentaPorProcesarService>();
builder.Services.AddSingleton<ICorreoService, CorreoService>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
    context.Response.StatusCode = StatusCodes.Status303SeeOther;
    context.Response.ContentType = "application/json";
    var jsonString = JsonConvert.SerializeObject(exception.Message);
    await context.Response.WriteAsync(jsonString);
}));

app.UseCors("CorsPolicy");

app.UseCustomAuthorization();

app.MapControllers();

app.Run();