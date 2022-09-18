using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleHouseInfo.Application.Features.Queries;

namespace SampleHouseInfo.API.Controllers;


/// <summary>
/// Class RoomsController
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{

  private IMediator _mediator;


  /// <summary>
  /// Initializes a new instance of the <see cref="RoomsController"/> class.
  /// </summary>
  public RoomsController(IMediator mediator)
  {
    _mediator = mediator;
  }


  /// <summary>
  /// Gets all rooms.
  /// </summary>
  /// <returns>All rooms</returns>
  [HttpGet]
  public async Task<IActionResult> Get()
  {
    return Ok(await _mediator.Send(new GetAllRoomsQuery()));
  }


  /// <summary>
  /// Gets a specified room by id.
  /// </summary>
  /// <param name="id">The id of the room.</param>
  [HttpGet("{id}")]
  public async Task<IActionResult> Get(string id)
  {
    return Ok(await _mediator.Send(new GetRoomByIdQuery { Id = id }));

  }
}
