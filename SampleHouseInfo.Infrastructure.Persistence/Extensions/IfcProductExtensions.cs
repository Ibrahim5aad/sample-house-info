using System.Linq;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;

namespace SampleHouseInfo.Infrastructure.Persistence.Extensions;


/// <summary>
/// Class IfcProductExtensions
/// </summary>
internal static class IfcProductExtensions
{

  /// <summary>
  /// Looks up a property in from a product property sets.
  /// </summary>
  /// <typeparam name="TPropType">The type of the property.</typeparam>
  /// <param name="ifcProduct">The ifc product.</param>
  /// <param name="name">The name.</param>
  /// <returns></returns>
  public static TPropType? LookupProperty<TPropType>(this IfcProduct ifcProduct, string name)
                           where TPropType : class, IIfcProperty

  {
    return ifcProduct.IsDefinedBy.Select(r =>
     {
       if (r.RelatingPropertyDefinition is IIfcPropertySet set &&
         set.HasProperties.Any(prop => prop is TPropType && prop.Name == name))
       {
         return set.HasProperties.First
             (prop => prop is TPropType && prop.Name == name) as TPropType;
       }
       return null;

     }).FirstOrDefault(f => f != null);
  }

}
