using SampleHouseInfo.Infrastructure.Persistence.Extensions;
using Serilog;
using ILogger = Serilog.ILogger;


namespace SampleHouseInfo.API;

/// <summary>
/// Class Program
/// </summary>
public class Program
{

  /// <summary>
  /// Defines the entry point of the application.
  /// </summary>
  /// <param name="args">The arguments.</param>
  public static async Task Main(string[] args)
  {
    var configuration = GetConfiguration();
    Log.Logger = CreateSerilogLogger(configuration);

    Log.Information("Configuring host...");
    var app = CreateHostBuilder(args).Build();

    Log.Information("Starting host...");
    app.Run();

  }


  /// <summary>
  /// Creates the host builder.
  /// </summary>
  /// <param name="args">The arguments.</param>
  /// <returns></returns>
  private static IHostBuilder CreateHostBuilder(string[] args)
  {
    var hostBuilder = Host.CreateDefaultBuilder(args)
                          .UseSerilog()
                          .ConfigureWebHostDefaults(whb =>
                          {
                            whb.UseStartup<Startup>()
                               .UseIISIntegration();
                          });
    return hostBuilder;
  }


  /// <summary>
  /// Creates the serilog logger.
  /// </summary>
  /// <param name="configuration">The configuration.</param>
  /// <returns></returns>
  private static ILogger CreateSerilogLogger(IConfiguration configuration)
  {
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .WriteTo.Console()
        .WriteTo.File("Logs/SampleHouseLogs-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    logger.AddAsXbimLogger();

    return logger;
  }


  /// <summary>
  /// Gets the configuration.
  /// </summary>
  /// <returns></returns>
  private static IConfiguration GetConfiguration()
  {
    string hotingEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{hotingEnvironment}.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    var config = builder.Build();
    return config;
  }

}



