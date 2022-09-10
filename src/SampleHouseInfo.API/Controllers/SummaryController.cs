using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleHouseInfo.Application.Features.Queries;

namespace SampleHouseInfo.API.Controllers;


/// <summary>
/// Class SummaryController
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class SummaryController : ControllerBase
{

  private IMediator _mediator;


  /// <summary>
  /// Initializes a new instance of the <see cref="RoomsController"/> class.
  /// </summary>
  public SummaryController(IMediator mediator)
  {
    _mediator = mediator;
  }


  /// <summary>
  /// Gets the instances count summary.
  /// </summary> 
  [HttpGet]
  [HttpGet("elementsCount")]
  public async Task<IActionResult> GetElementsCount()
  {
    return Ok(await _mediator.Send(new GetElementsCountQuery {}));

  }
}
