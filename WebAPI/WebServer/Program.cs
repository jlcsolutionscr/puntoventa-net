using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.ServicioWeb.Servicios;
using LeandroSoftware.ServicioWeb;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebServer.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

/*ILoggerProvider logProvider = new Log4NetProvider(new FileInfo("log4net.config").FullName);
builder.Services.AddLogging((builder) =>
{
    builder.ClearProviders();
    builder.AddProvider(logProvider).AddFilter(level => level >= LogLevel.Information);
});

ILogger logger = logProvider.CreateLogger("JLCPuntoventa");

builder.Services.AddSingleton(logger);*/

string strConnection = builder.Configuration.GetSection("connectionString").Value;

builder.Services.AddDbContext<LeandroContext>(x => x.UseMySQL(strConnection));
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

builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
    context.Response.StatusCode = StatusCodes.Status303SeeOther;
    context.Response.ContentType = "application/json";
    var jsonString = JsonConvert.SerializeObject(exception.Message);
    await context.Response.WriteAsync(jsonString);
}));

app.UseCors();

app.UseCustomAuthorization();

app.MapControllers();

app.Run();
