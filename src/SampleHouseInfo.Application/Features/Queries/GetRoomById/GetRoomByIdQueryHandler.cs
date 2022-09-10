using AutoMapper;
using MediatR;
using SampleHouseInfo.Application.Exceptions;
using SampleHouseInfo.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SampleHouseInfo.Application.Features.Queries;


/// <summary>
/// Class GetRoomByIdQueryHandler
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{SampleHouseInfo.Application.Features.Queries.GetRoomByIdQuery, SampleHouseInfo.Application.Features.Queries.RoomDto};" />
public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, RoomDto>
{

  private IRoomsRepository _roomsRepository;
  private IMapper _mapper;


  /// <summary>
  /// Initializes a new instance of the <see cref="GetRoomByIdQueryHandler" /> class.
  /// </summary>
  /// <param name="roomsRepository">The rooms repository.</param>
  /// <param name="mapper">The mapper.</param>
  public GetRoomByIdQueryHandler(IRoomsRepository roomsRepository, IMapper mapper)
  {
    _roomsRepository = roomsRepository;
    _mapper = mapper;
  }


  /// <summary>
  /// Handles a GetRoomById query
  /// </summary>
  /// <param name="query">The query</param>
  /// <param name="cancellationToken">Cancellation token</param>
  /// <returns>
  /// Response from the request
  /// </returns>
  /// <exception cref="System.NotImplementedException"></exception>
  public async Task<RoomDto> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken)
  {
    var room = await _roomsRepository.GetByIdAsync(query.Id);

    if (room == null)
      throw new NotFoundException("Room", query.Id);

    return _mapper.Map<RoomDto>(room);
  }
}
