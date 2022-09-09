using SampleHouseInfo.Domain.Entities;

namespace SampleHouseInfo.Application.Interfaces.Repositories
{
  /// <summary>
  /// Interface IRoomsRepository
  /// </summary>
  /// <seealso cref="SampleHouseInfo.Application.Interfaces.IReadOnlyRepository{SampleHouseInfo.Domain.Entities.Room}" />
  public interface IRoomsRepository : IReadOnlyRepository<Room, string>
  {
  }
}
