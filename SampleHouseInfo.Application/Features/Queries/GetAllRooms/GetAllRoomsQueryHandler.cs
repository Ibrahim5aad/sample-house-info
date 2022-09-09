using AutoMapper;
using MediatR;
using SampleHouseInfo.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SampleHouseInfo.Application.Features.Queries;


/// <summary>
/// Class GetAllRoomsQueryHandler
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{SampleHouseInfo.Application.Features.Queries.GetAllRooms.GetAllRoomsQuery, System.Collections.Generic.IEnumerable{SampleHouseInfo.Application.Features.Queries.GetAllRooms.RoomDto}}" />
public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, IEnumerable<RoomDto>>
{

  private IRoomsRepository _roomsRepository;
  private IMapper _mapper;


  /// <summary>
  /// Initializes a new instance of the <see cref="GetAllRoomsQueryHandler" /> class.
  /// </summary>
  /// <param name="roomsRepository">The rooms repository.</param>
  public GetAllRoomsQueryHandler(IRoomsRepository roomsRepository, IMapper mapper)
  {
    _roomsRepository = roomsRepository;
    _mapper = mapper;
  }

  /// <summary>
  /// Handles a GetAllRooms query
  /// </summary>
  /// <param name="query">The query</param>
  /// <param name="cancellationToken">Cancellation token</param>
  /// <returns>
  /// Response from the request
  /// </returns>
  public async Task<IEnumerable<RoomDto>> Handle(GetAllRoomsQuery query, CancellationToken cancellationToken)
  {
    var rooms = await _roomsRepository.GetAllAsync();
    return _mapper.Map<IEnumerable<RoomDto>>(rooms); 
  }
}