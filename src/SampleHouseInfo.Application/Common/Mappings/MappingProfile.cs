using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace SampleHouseInfo.Application.Common.Mappings;

public class MappingProfile : Profile
{

  /// <summary>
  /// Initializes a new instance of the <see cref="MappingProfile"/> class.
  /// </summary>
  public MappingProfile()
  {
    ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
  }

  /// <summary>
  /// Applies the mappings from a specified assembly.
  /// </summary>
  /// <param name="assembly">The assembly.</param>
  private void ApplyMappingsFromAssembly(Assembly assembly)
  {
    var types = assembly.GetExportedTypes()
        .Where(t => t.GetInterfaces().Any(i =>
            i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
        .ToList();

    foreach (var type in types)
    {
      var instance = Activator.CreateInstance(type);
      var methodInfo = type.GetMethod("Mapping");
      methodInfo?.Invoke(instance, new object[] { this });
    }
  }
}
