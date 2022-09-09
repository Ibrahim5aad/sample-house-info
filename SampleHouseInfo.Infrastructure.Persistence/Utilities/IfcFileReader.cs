using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SampleHouseInfo.Application.Interfaces;
using SampleHouseInfo.Domain.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xbim.Common;
using Xbim.Ifc;

namespace SampleHouseInfo.Infrastructure.Persistence.Utilities;


/// <summary>
/// Class IfcFileUtility
/// </summary>
/// <seealso cref="System.IDisposable" />
internal class IfcFileReader : IDisposable
{

  #region Fields

  private AppSettings _settings;
  private ILogger<IfcFileReader> _logger;
  private IfcStore _store;

  #endregion

  #region Constructor

  /// <summary>
  /// Initializes a new instance of the <see cref="IfcFileReader" /> class.
  /// </summary>
  /// <param name="options">The options.</param>
  /// <param name="ILogger`1">The i logger`1.</param>
  public IfcFileReader(IOptions<AppSettings> options, ILogger<IfcFileReader> logger)
  {
    _settings = options.Value;
    _logger = logger;
    LoadFile();
  }

  #endregion

  #region Methods

  /// <summary>
  /// Loads the IFC file.
  /// </summary>
  private void LoadFile()
  {
    try
    {
      _logger.LogInformation("Loading the IFC file...");

      string fileName = Path.Combine
     (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
                           _settings.IfcFilePath);

      _store = IfcStore.Open(fileName);

      _logger.LogInformation("IFC file loaded successfully.");

    }
    catch (System.Exception e)
    {
      _logger.LogError(e, "Loading IFC file failed");
    }

  }
    

  /// <summary>
  /// Gets all IFC element of type T.
  /// </summary>
  /// <typeparam name="T"> The type of the elements </typeparam>
  /// <returns></returns>
  public IEnumerable<T> GetAll<T>()
                        where T : IPersistEntity
  {
    return _store.Instances.OfType<T>();
  }


  /// <summary>
  /// Gets all IFC elements of type TSource transformed to elements of type TTarget.
  /// </summary>
  /// <typeparam name="TSource">The type of the IFC elements.</typeparam>
  /// <typeparam name="TTarget">The type of the target elements.</typeparam>
  /// <param name="transformer">The transformer.</param>
  /// <returns></returns>
  public IEnumerable<TTarget> GetAll<TSource, TTarget>(Func<TSource, TTarget> transformer)
                              where TSource : IPersistEntity
                              where TTarget : class
  {
    return GetAll<TSource>()
            .Select(transformer);
  }


  /// <summary>
  /// Performs application-defined tasks associated with freeing,
  /// releasing, or resetting unmanaged resources.
  /// </summary>
  public void Dispose()
  {
    _store.Close();
  }

  #endregion


}
