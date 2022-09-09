namespace SampleHouseInfo.Application.Exceptions;

public class NotFoundException : ApiException
{
  /// <summary>
  /// Initializes a new instance of the <see cref="NotFoundException"/> class.
  /// </summary>
  /// <param name="name">The name.</param>
  /// <param name="id">The id.</param>
  public NotFoundException(string name, object id)
      : base($"Entity \"{name}\" ({id}) was not found.")
  {
  }
}
