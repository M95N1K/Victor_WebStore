using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using Victor_WebStore.DAL;

namespace Victor_WebStore
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(host => host
                .UseStartup<Startup>()
                .UseSerilog((host, log) => log.ReadFrom.Configuration(host.Configuration)
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft",LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.RollingFile($"./Log/Struct/{DateTime.Now:yyyy-MM-dd}.log")
                .WriteTo.File(new JsonFormatter(",",true),$"./Log/Struct/{DateTime.Now:yyyy-MM-dd}.log.json")
                ));
    }
}
