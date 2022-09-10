using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SampleHouseInfo.Application.Extensions;


/// <summary>
/// Class ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{

  /// <summary>
  /// Registers the application layer services to the IoC container.
  /// </summary>
  /// <param name="services">The services.</param>
  public static void RegisterApplicationLayerServices(this IServiceCollection services)
  {
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    services.AddMediatR(Assembly.GetExecutingAssembly());
  }

}
