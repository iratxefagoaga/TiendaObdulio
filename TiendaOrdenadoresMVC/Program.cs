using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.Data;
using MVC_ComponentesCodeFirst.Logging;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.API_Repositories;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using NLog;
using Polly;
using Polly.Extensions.Http;

namespace MVC_ComponentesCodeFirst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<OrdenadoresContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("OrdenadoresContext")
                    ?.Replace("[DataDirectory]", Directory.GetCurrentDirectory())));

            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

            // Create the retry policy we want
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError() // HttpRequestException, 5XX and 408
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

            // register the Service and apply the retry policy
            builder.Services.AddHttpClient<IClienteRepository, ClienteApiRepository>(o =>
                    o.BaseAddress = new Uri("https://localhost:7135/api/"))
                .AddPolicyHandler(retryPolicy);
            builder.Services.AddHttpClient<IComponenteRepository, ComponenteApiRepository>(o =>
                    o.BaseAddress = new Uri("https://localhost:7135/api/"))
                .AddPolicyHandler(retryPolicy);
            builder.Services.AddHttpClient<IOrdenadorRepository, OrdenadorApiRepository>(o =>
                    o.BaseAddress = new Uri("https://localhost:7135/api/"))
                .AddPolicyHandler(retryPolicy);
            builder.Services.AddHttpClient<IPedidoRepository, PedidoApiRepository>(o =>
                    o.BaseAddress = new Uri("https://localhost:7135/api/"))
                .AddPolicyHandler(retryPolicy);
            builder.Services.AddHttpClient<IFacturasRepository, FacturaApiRepository>(o =>
                    o.BaseAddress = new Uri("https://localhost:7135/api/"))
                .AddPolicyHandler(retryPolicy);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SeedData.Initialize(services.GetRequiredService<IComponenteRepository>(), services.GetRequiredService<IOrdenadorRepository>(), services.GetRequiredService<IPedidoRepository>(), services.GetRequiredService<IFacturasRepository>(), services.GetRequiredService<IClienteRepository>());
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=First}/{action=Index}/{id?}");

            app.Run();
        }
    }
}