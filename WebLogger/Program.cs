using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebLogger
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("credentials.json", true, reloadOnChange: true).Build();


            var logs = new DocumentStore()
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "WebLogger",
            }.Initialize();

            Log.Logger = new LoggerConfiguration()
                       //.MinimumLevel.Warning()
                       .MinimumLevel.ControlledBy(LoggingFeature.loggingLevel)
                       .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                       //.Enrich.With(new ThreadEnricher())
                       .WriteTo.Console()
                       .WriteTo.File(@"C:\Users\BS581\source\repos\WebLogger\WebLogger\log.txt")
                       .WriteTo.RavenDB(logs)
                       .CreateLogger();

       

            CreateHostBuilder(args).Build().Run();

            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }


    public class ThreadEnricher: ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory
                .CreateProperty("ThreadId", Thread.CurrentThread.ManagedThreadId));
        }
    }

    public static class LoggingFeature
    {
        public static LoggingLevelSwitch loggingLevel => new LoggingLevelSwitch(LogEventLevel.Information);
    }
}
