using Microsoft.Extensions.DependencyInjection;
using SampleHouseInfo.Application.Interfaces;
using SampleHouseInfo.Application.Interfaces.Repositories;
using SampleHouseInfo.Infrastructure.Persistence.Data;
using SampleHouseInfo.Infrastructure.Persistence.Interfaces;
using SampleHouseInfo.Infrastructure.Persistence.Repository;
using SampleHouseInfo.Infrastructure.Persistence.Services;

namespace SampleHouseInfo.Infrastructure.Persistence.Extensions;


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
    services.AddScoped<IRoomsRepository, RoomsRepository>();
    services.AddScoped<ISummaryService, SummaryService>();
    services.AddSingleton<IIfcXbimProvider, IfcXbimProvider>();
  }

}
