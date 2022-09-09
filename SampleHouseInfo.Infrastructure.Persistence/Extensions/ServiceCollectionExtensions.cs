using Microsoft.Extensions.DependencyInjection;
using SampleHouseInfo.Application.Interfaces.Repositories;
using SampleHouseInfo.Infrastructure.Persistence.Repository;
using SampleHouseInfo.Infrastructure.Persistence.Utilities;

namespace SampleHouseInfo.Infrastructure.Persistence.Extensions
{
  /// <summary>
  /// Class ServiceCollectionExtensions
  /// </summary>
  public static class ServiceCollectionExtensions
  {

    /// <summary>
    /// Registers the persistence layer services to the IoC container.
    /// </summary>
    /// <param name="services">The services.</param>
    public static void RegisterPersistenceLayerServices(this IServiceCollection services)
    {
      services.AddTransient<IRoomsRepository, RoomsRepository>();
      services.AddSingleton<IfcFileReader>();
    }

  }
}
