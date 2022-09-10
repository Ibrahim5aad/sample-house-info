using SampleHouseInfo.Application.Interfaces.Repositories;
using SampleHouseInfo.Domain.Entities;
using SampleHouseInfo.Infrastructure.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xbim.Ifc4.ProductExtension;

namespace SampleHouseInfo.Infrastructure.Persistence.Repository;


/// <summary>
/// Class RoomsRepository
/// </summary>
/// <seealso cref="SampleHouseInfo.Application.Interfaces.Repositories.IRoomsRepository" />
internal class RoomsRepository : IRoomsRepository
{

  #region Fields

  private readonly IEnumerable<Room> _rooms;
  private readonly IIfcXbimProvider _ifcProvider;

  #endregion

  #region Constructors

  /// <summary>
  /// Initializes a new instance of the <see cref="RoomsRepository" /> class.
  /// </summary>
  /// <param name="ifcProvider">The ifc provider.</param>
  public RoomsRepository(IIfcXbimProvider ifcProvider)
  {
    _ifcProvider = ifcProvider;

    _rooms = _ifcProvider.GetAllOfType<IfcSpace, Room>((space) =>
    {
      return new Room
      {
        Name = space.Name!,
        Id = space.GlobalId.Value.ToString()!,
        NetArea = Convert.ToDouble(space.NetFloorArea?.Value ?? 0d),
      };
    });
  }

  #endregion

  #region Methods

  /// <summary>
  /// Gets all rooms.
  /// </summary>
  /// <returns></returns>
  public async Task<IReadOnlyList<Room>> GetAllAsync()
  {
    return await Task.Run(() => _rooms.ToList());
  }


  /// <summary>
  /// Gets a rom by id.
  /// </summary>
  /// <param name="id">The id.</param>
  /// <returns></returns>
  public async Task<Room> GetByIdAsync(string id)
  {
    return await Task.Run(() => _rooms.FirstOrDefault(r => r.Id == id));
  }


  /// <summary>
  /// Gets a paged reponse.
  /// </summary>
  /// <param name="pageNumber">The page number.</param>
  /// <param name="pageSize">Size of the page.</param>
  /// <returns></returns> 
  public async Task<IReadOnlyList<Room>> GetPagedReponseAsync(int pageNumber, int pageSize)
  {
    return await Task.Run(() => _rooms.Skip((pageNumber - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToList());
  }

  #endregion

}
