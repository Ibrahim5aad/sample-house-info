using System;
using System.Globalization;

namespace SampleHouseInfo.Application.Exceptions;


/// <summary>
/// Class ApiException
/// </summary>
/// <seealso cref="System.Exception" />
public class ApiException : Exception
{
  #region Constructors

  /// <summary>
  /// Initializes a new instance of the <see cref="ApiException"/> class.
  /// </summary>
  public ApiException() : base() { }


  /// <summary>
  /// Initializes a new instance of the <see cref="ApiException"/> class.
  /// </summary>
  /// <param name="message">The message that describes the error.</param>
  public ApiException(string message) : base(message) { }


  /// <summary>
  /// Initializes a new instance of the <see cref="ApiException"/> class.
  /// </summary>
  /// <param name="message">The message.</param>
  /// <param name="args">The arguments.</param>
  public ApiException(string message, params object[] args)
      : base(String.Format(CultureInfo.CurrentCulture, message, args))
  {
  }

  #endregion
}
