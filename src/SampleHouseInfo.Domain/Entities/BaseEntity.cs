namespace SampleHouseInfo.Domain.Entities;

/// <summary>
/// Class BaseEntity
/// </summary>
/// <typeparam name="TId">The type of the id.</typeparam>
public abstract class BaseEntity<TId>
{

  /// <summary>
  /// Gets or sets the identifier.
  /// </summary>
  /// <value>
  /// The identifier.
  /// </value>
  public TId Id { get; set; }
}
