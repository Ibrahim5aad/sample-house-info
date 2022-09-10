using SampleHouseInfo.Application.Interfaces;
using SampleHouseInfo.Infrastructure.Persistence.Extensions;
using SampleHouseInfo.Infrastructure.Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;

namespace SampleHouseInfo.Infrastructure.Persistence.Services;


/// <summary>
/// Class SummaryService
/// </summary>
/// <seealso cref="SampleHouseInfo.Application.Interfaces.ISummaryService" />
internal class SummaryService : ISummaryService
{

  #region Fields

  private IIfcXbimProvider _ifcProvider;

  #endregion

  #region Constructors

  /// <summary>
  /// Initializes a new instance of the <see cref="SummaryService"/> class.
  /// </summary>
  /// <param name="ifcProvider">The ifc provider.</param>
  public SummaryService(IIfcXbimProvider ifcProvider)
  {
    _ifcProvider = ifcProvider;
  }

  #endregion

  #region Methods

  /// <summary>
  /// Gets the elements count.
  /// </summary>
  /// <returns></returns>
  public async Task<Dictionary<string, int>> GetElementsCountAsync()
  {

    // based on this query I'm getting all the elements that has a 'Category'
    // property, which is not guranteed to work woth other files

    // A more stable way that is based on the IFC elements themselves
    // is to get all the ifc products and group by its type name

    //var counts = await Task.Run
    //    (() => (Dictionary<string, int>)_ifcProvider
    //          .Map<IfcProduct, KeyValuePair<string, int>>((products) =>
    //          {
    //            return products.GroupBy(i => i.GetType().Name.Replace("Ifc", "")).ToDictionary(g => g.Key, g => g.Count());
    //          }));

    return await Task.Run
        (() => (Dictionary<string, int>)_ifcProvider
              .Map<IfcProduct, KeyValuePair<string, int>>((products) =>
              {
                return products.GroupBy(i => i.LookupProperty<IIfcPropertySingleValue>("Category"))
                                    .ToDictionary(i => i.Key?.NominalValue.ToString() ?? "Uncategorized Elements", i => i.Count());
              }));
  }

  #endregion

}
