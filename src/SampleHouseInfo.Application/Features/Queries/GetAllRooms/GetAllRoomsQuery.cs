using MediatR;
using System.Collections.Generic;

namespace SampleHouseInfo.Application.Features.Queries;


/// <summary>
/// Class GetAllRoomsQuery
/// </summary>
/// <seealso cref="MediatR.IRequest{System.Collections.Generic.IEnumerable{SampleHouseInfo.Application.Features.Queries.GetAllRooms.RoomDto}}" />
public class GetAllRoomsQuery : IRequest<IEnumerable<RoomDto>>
{
}