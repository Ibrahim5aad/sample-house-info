using Moq;
using NUnit.Framework;
using SampleHouseInfo.Application.Features.Queries;
using SampleHouseInfo.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests;


[TestFixture]
public class SummaryQueriesTests
{

  #region Fields

  private Dictionary<string, int> _dummySummary;

  #endregion

  #region Setup

  /// <summary>
  /// Setups this test fixture.
  /// </summary>
  [SetUp]
  public void Setup()
  {
    SetupDummySummary();
  }


  /// <summary>
  /// Setups the dummy summary.
  /// </summary>
  private void SetupDummySummary()
  {
    _dummySummary = new Dictionary<string, int>()
    {
      { "Room", 4 },
      { "Window", 12 },
      { "Door", 5 },
      { "Wall", 12 },
    };
  }

  #endregion

  #region Tests

  [Test]
  public async Task GetElementsCountsQuery()
  {

    //Arrange

    var summarServiceMock = new Mock<ISummaryService>();

    summarServiceMock.Setup(r => r.GetElementsCountAsync())
                  .ReturnsAsync(() => _dummySummary);

    var queryHandler = new GetElementsCountQueryHandler(summarServiceMock.Object);

    //Act

    var result = await queryHandler.Handle
          (new GetElementsCountQuery(), CancellationToken.None);

    //Assert

    Assert.AreEqual(result.Count(), _dummySummary.Count);
    for (int i = 0; i < _dummySummary.Count; i++)
    {
      var source = _dummySummary.ElementAt(i);
      var target = result.ElementAt(i);

      Assert.AreEqual(source.Key, target.Key);
      Assert.AreEqual(source.Value, target.Value);

    }
  }


  #endregion

}
