using MediatR;

namespace SampleHouseInfo.Application.Features.Queries;


/// <summary>
/// Class GetRoomByIdQuery
/// </summary>
/// <seealso cref="MediatR.IRequest{SampleHouseInfo.Application.Features.Queries.GetAllRooms.RoomDto}" />
public class GetRoomByIdQuery : IRequest<RoomDto>
{

  /// <summary>
  /// Gets or sets the identifier.
  /// </summary>
  /// <value>
  /// The identifier.
  /// </value>
  public string Id { get; set; }
}