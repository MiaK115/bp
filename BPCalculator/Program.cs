using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace BPCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Read optional Seq configuration from environment
            var seqUrl = Environment.GetEnvironmentVariable("SEQ_URL");            // e.g. http://localhost:5341 (local dev) or your Seq Cloud URL
            var seqApiKey = Environment.GetEnvironmentVariable("SEQ_API_KEY");     // optional
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console();

            // Only write a local rolling file when developing
            if (string.Equals(env, "Development", StringComparison.OrdinalIgnoreCase))
            {
                loggerConfig.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day);
            }

            // Only enable Seq when SEQ_URL is set (don’t set this on Render unless you have a public Seq)
            if (!string.IsNullOrWhiteSpace(seqUrl))
            {
                if (!string.IsNullOrWhiteSpace(seqApiKey))
                    loggerConfig.WriteTo.Seq(serverUrl: seqUrl, apiKey: seqApiKey);
                else
                    loggerConfig.WriteTo.Seq(serverUrl: seqUrl);
            }

            Log.Logger = loggerConfig.CreateLogger();

            try
            {
                Log.Information("Starting up the application");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() // <-- important: plug Serilog into ASP.NET Core logging
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}