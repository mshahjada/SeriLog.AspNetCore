using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebLogger.Config;

namespace WebLogger
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                        //.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, reloadOnChange: true).Build();

            var loggerSettings = configuration.GetSection(nameof(LoggerSettings)).Get<LoggerSettings>();


             var docStore = new DocumentStore() { Urls = loggerSettings.RavenDBSink.Urls, Database = loggerSettings.RavenDBSink.Database }.Initialize();

            Log.Logger = new LoggerConfiguration()
                       .MinimumLevel.ControlledBy(LoggingFeature.loggingLevel)
                       .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)

                        //.Enrich.With(new ThreadEnricher())
                        //.Enrich.FromLogContext()
                       .Enrich.WithProperty("TestLog", "Test")
                       .Enrich.WithEnvironmentName()
                       .Enrich.WithThreadId()
                       .Enrich.WithTestd()
    
                       .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                       .WriteTo.File(loggerSettings.FilePath, rollingInterval: RollingInterval.Minute)
                       .WriteTo.RavenDB(docStore)
                       //.WriteTo.Seq(loggerSettings.SeqUrl)
                       .CreateLogger();


            Serilog.Debugging.SelfLog.Enable(Console.Error);

            CreateHostBuilder(args).Build().Run();

            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                  .UseSerilog()
                //.UseSerilog((ctx, conf) =>
                //{
                //    conf
                //   .ReadFrom.Configuration(ctx.Configuration)
                //   .Enrich.FromLogContext()
                //   .Enrich.WithMachineName()
                //   .Enrich.WithThreadId();
                //    //.Enrich.WithProperty("WithMachineName", Environment.MachineName)
                //    //.Enrich.WithProperty("WithThreadId", Thread.CurrentThread.ManagedThreadId)
                //})
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

    public class TestEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(propertyFactory
                .CreateProperty("TestLog", "Hello"));
        }
    }

    public static class CustomEnrich
    {
        public static LoggerConfiguration WithThreadId(this LoggerEnrichmentConfiguration enrich)
        {
            return enrich.With<ThreadEnricher>();
        }

        public static LoggerConfiguration WithTestd(this LoggerEnrichmentConfiguration enrich)
        {
            return enrich.With<TestEnricher>();
        }
    }

    public static class LoggingFeature
    {
        public static LoggingLevelSwitch loggingLevel => new LoggingLevelSwitch(LogEventLevel.Warning);
    }
}
