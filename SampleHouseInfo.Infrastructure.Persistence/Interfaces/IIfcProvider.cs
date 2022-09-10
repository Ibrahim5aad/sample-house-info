using System;
using System.Collections.Generic;

namespace SampleHouseInfo.Infrastructure.Persistence.Interfaces;


/// <summary>
/// Interface IIfcProvider
/// </summary>
/// <typeparam name="TPersistedBase">The type of the persisted base.</typeparam>
public interface IIfcProvider<TPersistedBase>
{

  IEnumerable<TTarget> Map<TSource, TTarget>(Func<IEnumerable<TSource>, IEnumerable<TTarget>> transformer)
                              where TSource : TPersistedBase;

  IEnumerable<T> GetAllOfType<T>()
                 where T : TPersistedBase;

  IEnumerable<TTarget> GetAllOfType<TSource, TTarget>(Func<TSource, TTarget> transformer)
                       where TSource : TPersistedBase
                       where TTarget : class;

}
