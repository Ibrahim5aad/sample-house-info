using MediatR;
using SampleHouseInfo.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SampleHouseInfo.Application.Features.Queries;


/// <summary>
/// Class GetElementsCountQuery
/// </summary>
/// <seealso cref="MediatR.IRequest{System.Collections.Generic.Dictionary{System.String, System.Int32}}" />
public class GetElementsCountQueryHandler : IRequestHandler<GetElementsCountQuery, Dictionary<string, int>>
{
  private ISummaryService _summaryService;

  /// <summary>
  /// Initializes a new instance of the <see cref="GetElementsCountQueryHandler"/> class.
  /// </summary>
  public GetElementsCountQueryHandler(ISummaryService summaryService)
  {
    _summaryService = summaryService;
  }


  /// <summary>
  /// Handles the specified GetElementsCount query.
  /// </summary>
  /// <param name="query">The query.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns></returns>
  public Task<Dictionary<string, int>> Handle(GetElementsCountQuery query, CancellationToken cancellationToken)
  {
    return _summaryService.GetElementsCountAsync();
  }
}