using api_fact_weather_by_city.Logs;
using DatabaseLayer.Context;
using Static.Service;
 
namespace ServerRestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            ServiceHandler.provider = host.Services;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) 
        {
            var builder = Host.CreateDefaultBuilder(args);

            Action<ILoggingBuilder> a;
            a = loggingBuilder => {
                //loggingBuilder.ClearProviders();
                //loggingBuilder.AddConsole();
                loggingBuilder.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "Logs\\Logs.txt"));
            };

            builder.ConfigureLogging(a);


            var host = builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                .ConfigureKestrel((context, options) => { options.AllowSynchronousIO = true; });
                
            });

            return host;
        }

    }
}