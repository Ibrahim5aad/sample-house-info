using MediatR;
using System.Collections.Generic;

namespace SampleHouseInfo.Application.Features.Queries;


/// <summary>
/// Class GetElementsCountQuery
/// </summary>
/// <seealso cref="MediatR.IRequest{System.Collections.Generic.Dictionary{System.String, System.Int32}}" />
public class GetElementsCountQuery : IRequest<Dictionary<string, int>>
{
}