using AutoMapper;

namespace SampleHouseInfo.Application.Common.Mappings;


/// <summary>
/// Interface IMapFrom is used to encapsulate the mapping configuration
/// for each mapped object within the object itself.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IMapFrom<T>
{
  /// <summary>
  /// Defines the required mapping configuration for this instance.
  /// </summary>
  /// <param name="profile">The profile.</param>
  void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
