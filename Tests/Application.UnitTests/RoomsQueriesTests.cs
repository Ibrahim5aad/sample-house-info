using AutoMapper;
using Moq;
using NUnit.Framework;
using SampleHouseInfo.Application.Common.Mappings;
using SampleHouseInfo.Application.Exceptions;
using SampleHouseInfo.Application.Features.Queries;
using SampleHouseInfo.Application.Interfaces.Repositories;
using SampleHouseInfo.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests;


[TestFixture]
public class RoomsQueriesTests
{

  #region Fields

  private List<Room> _rooms;
  private IMapper _mapper;

  #endregion

  #region Setup

  /// <summary>
  /// Setups this test fixture.
  /// </summary>
  [OneTimeSetUp]
  public void Setup()
  {
    SetupDummyRooms();
    SetupMapper();

  }

  /// <summary>
  /// Setups the mapper.
  /// </summary>
  private void SetupMapper()
  {
    var configurationProvider = new MapperConfiguration(cfg =>
    {
      cfg.AddProfile<MappingProfile>();
    });

    _mapper = configurationProvider.CreateMapper();
  }

  /// <summary>
  /// Setups the dummy rooms.
  /// </summary>
  private void SetupDummyRooms()
  {
    _rooms = new List<Room>()
      {
        new Room{ Id = "1", Name = "Room 1", NetArea = 12d },
        new Room{ Id = "2", Name = "Room 2", NetArea = 10d },
        new Room{ Id = "3", Name = "Room 3", NetArea = 9d },
        new Room{ Id = "4", Name = "Room 4", NetArea = 13d },
      };
  }

  #endregion

  #region Tests

  [Test]
  public async Task GetAllRoomsQuery_ShouldReturnRoomsCountEqualToDummyRooms()
  {

    //Arrange

    var roomsRepoMock = new Mock<IRoomsRepository>();

    roomsRepoMock.Setup(r => r.GetAllAsync())
                  .ReturnsAsync(_rooms);

    var queryHandler = new GetAllRoomsQueryHandler(roomsRepoMock.Object, _mapper);

    //Act

    var result = await queryHandler.Handle
          (new GetAllRoomsQuery(), CancellationToken.None);

    //Assert

    Assert.AreEqual(result.Count(), _rooms.Count);
  }


  [Test]
  public async Task GetRoomByIdQuery_GivenValidId_ShouldReturnTheRoomWithThisId()
  {

    //Arrange

    var dummyId = "1";

    var roomsRepoMock = new Mock<IRoomsRepository>();

    roomsRepoMock.Setup(r => r.GetByIdAsync(dummyId))
                  .ReturnsAsync(_rooms.Find(r => r.Id == dummyId));

    var queryHandler = new GetRoomByIdQueryHandler
                              (roomsRepoMock.Object, _mapper);

    //Act

    var result = await queryHandler.Handle
          (new GetRoomByIdQuery() { Id = dummyId }, CancellationToken.None);

    //Assert

    Assert.AreEqual(result.Id, dummyId);
  }


  [Test]
  public async Task GetRoomByIdQuery_GivenInvalidId_ShouldReturnNull()
  {

    //Arrange

    var dummyId = "20";

    var roomsRepoMock = new Mock<IRoomsRepository>();

    roomsRepoMock.Setup(r => r.GetByIdAsync(dummyId))
                  .ReturnsAsync(_rooms.Find(r => r.Id == dummyId));

    var queryHandler = new GetRoomByIdQueryHandler
                              (roomsRepoMock.Object, _mapper);

    //Assert

    Assert.ThrowsAsync<NotFoundException>
        (async () => await queryHandler.Handle
            (new GetRoomByIdQuery() { Id = dummyId }, CancellationToken.None));
  }
  #endregion

}
