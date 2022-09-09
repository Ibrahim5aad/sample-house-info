using SampleHouseInfo.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace SampleHouseInfo.API.Middlewares;


/// <summary>
/// Class ErrorHandlerMiddleware; a request pipeline middleware to interrupt
/// any thrown response exceptions and return a user-friendly message with 
/// the correct status code
/// </summary>
public class ErrorHandlerMiddleware
{

  private readonly RequestDelegate _next;


  /// <summary>
  /// Initializes a new instance of the <see cref="ErrorHandlerMiddleware"/> class.
  /// </summary>
  /// <param name="next">The next.</param>
  public ErrorHandlerMiddleware(RequestDelegate next)
  {
    _next = next;
  }


  /// <summary>
  /// Invokes the specified context.
  /// </summary>
  /// <param name="context">The context.</param>
  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception error)
    {
      var response = context.Response;
      response.ContentType = "application/json";

      switch (error)
      {
        case NotFoundException:
          response.StatusCode = (int)HttpStatusCode.NotFound;
          break;
        case ApiException:
          response.StatusCode = (int)HttpStatusCode.BadRequest;
          break;
        default:
          response.StatusCode = (int)HttpStatusCode.InternalServerError;
          break;
      }

      var result = JsonSerializer.Serialize(new { Message = error.Message! });

      await response.WriteAsync(result);
    }
  }
}
