using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleHouseInfo.Application.Interfaces;

public interface ISummaryService
{

/// <summary>
/// Gets the elements count.
/// </summary>
/// <returns></returns>
Task<Dictionary<string, int>> GetElementsCountAsync();

}
