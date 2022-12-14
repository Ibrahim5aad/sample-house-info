namespace SampleHouseInfo.Domain.Entities;

public class Room : BaseEntity<string>
{

  /// <summary>
  /// Gets or sets the name.
  /// </summary>
  /// <value>
  /// The name.
  /// </value>
  public string Name { get; set; }

  /// <summary>
  /// Gets or sets the net area.
  /// </summary>
  /// <value>
  /// The area.
  /// </value>
  public double NetArea { get; set; }

  public double GrossArea { get; set; }

}
