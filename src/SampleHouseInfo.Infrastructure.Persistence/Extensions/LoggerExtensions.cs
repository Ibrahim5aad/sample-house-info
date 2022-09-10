using Serilog;
using Xbim.Common;

namespace SampleHouseInfo.Infrastructure.Persistence.Extensions;


/// <summary>
/// Class PersistenceInfrastructure
/// </summary>
public static class LoggerExtensions
{

  /// <summary>
  /// Adds the specified Serilog as xbim logger.
  /// </summary>
  public static void AddAsXbimLogger(this ILogger logger)
  {
    XbimLogging.LoggerFactory.AddSerilog(logger);
  }

}
