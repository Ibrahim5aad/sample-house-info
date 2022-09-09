using AutoMapper;

namespace SampleHouseInfo.Application.Common.Mappings;


/// <summary>
/// Interface IMapFrom
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IMapFrom<T>
{
  void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
