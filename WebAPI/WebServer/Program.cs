using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.ServicioWeb.Servicios;
using LeandroSoftware.ServicioWeb;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

/*ILoggerProvider logProvider = new Log4NetProvider(new FileInfo("log4net.config").FullName);
builder.Services.AddLogging((builder) =>
{
    builder.ClearProviders();
    builder.AddProvider(logProvider).AddFilter(level => level >= LogLevel.Information);
});

ILogger logger = logProvider.CreateLogger("JLCPuntoventa");

builder.Services.AddSingleton(logger);*/

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

builder.Services.AddDbContext<LeandroContext>(x => x.UseMySQL(config.GetSection("connectionString").Value));
builder.Services.AddScoped<ILeandroContext, LeandroContext>();
builder.Services.AddScoped<ICorreoService, CorreoService>();
builder.Services.AddScoped<IMantenimientoService, MantenimientoService>();
builder.Services.AddScoped<IFacturacionService, FacturacionService>();
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<IFlujoCajaService, FlujoCajaService>();
builder.Services.AddScoped<IBancaService, BancaService>();
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<IContabilidadService, ContabilidadService>();
builder.Services.AddScoped<ITrasladoService, TrasladoService>();
builder.Services.AddScoped<ICuentaPorProcesarService, CuentaPorProcesarService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerPathFeature>()
        .Error;
    context.Response.StatusCode = StatusCodes.Status303SeeOther;
    context.Response.ContentType = "application/json";
    var jsonString = JsonConvert.SerializeObject(exception.Message);
    await context.Response.WriteAsync(jsonString);
}));

app.Run();
