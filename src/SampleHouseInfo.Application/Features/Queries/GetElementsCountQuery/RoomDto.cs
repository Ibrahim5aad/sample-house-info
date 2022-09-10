using AutoMapper;
using SampleHouseInfo.Application.Common.Mappings;
using SampleHouseInfo.Domain.Entities;

namespace SampleHouseInfo.Application.Features.Queries;


/// <summary>
/// Class RoomDto
/// </summary>
public class ElementsCountDto : IMapFrom<Room>
{

  public string Id { get; set; }
  public string Name { get; set; }
  public double Area { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Room, RoomDto>()
        .ForMember(d => d.Area, opt => opt.MapFrom(s => s.NetArea));
  }

}
