using API.IntegrationTests.Common;
using NUnit.Framework;
using SampleHouseInfo.API;
using SampleHouseInfo.Application.Features.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.UnitTests;


/// <summary>
/// Class RoomsControllerTests
/// </summary>
public class RoomsControllerTests
{

  private HttpClient _client;


  [SetUp]
  public void Setup()
  {
    var _factory = new CustomWebApplicationFactory<Startup>();
    _client = _factory.CreateClient();
  }
   

  [Test]
  public async Task RoomsController_GetAll_ShouldReturnFourRooms()
  {
    // Act
    var response = await _client.GetAsync($"/api/rooms"); 
    var rooms = await Utilities.GetResponseContent<IEnumerable<RoomDto>>(response);

    // Assert
    response.EnsureSuccessStatusCode();
    Assert.IsNotEmpty(rooms);
    Assert.That(4, Is.EqualTo(rooms.Count()));
  }


  [Test]
  public async Task RoomsController_GetById_ShouldReturnCorrectRoom()
  {

    // Arrange
    var id = "3w0zWKm7n8SB1qbfwUzt0U";

    // Act
    var response = await _client.GetAsync($"/api/rooms/{id}");
    var room = await Utilities.GetResponseContent<RoomDto>(response);

    // Assert
    response.EnsureSuccessStatusCode(); 
    Assert.IsNotNull(room.Id);
    Assert.That(id, Is.EqualTo(room.Id));
  }


  [Test]
  public async Task RoomsController_GetById_ShouldReturnNotFound()
  {

    // Arrange
    var id = "3w0zWKm7nasffwUzt0U";

    // Act
    var response = await _client.GetAsync($"/api/rooms/{id}");
    var room = await Utilities.GetResponseContent<RoomDto>(response);

    // Assert 
    Assert.IsNull(room.Id);
    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
  }
}
