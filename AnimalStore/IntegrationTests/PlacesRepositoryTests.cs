using AnimalStore.Data.Repositories.Places;
using MongoDB.Driver;
using NUnit.Framework;
using System.Linq;

namespace IntegrationTests
{
  [TestFixture]
  public class PlacesRepositoryTests
  {
    private PlacesRepository _placesRepository;

    [TestFixtureSetUp]
    public void SetUp()
    {
      var mongoClient = new MongoClient("Server=localhost:27017");

      _placesRepository = new PlacesRepository(mongoClient);
    }

    [Test]
    public void GetAll_Returns_All_Places()
    {
      var allplaces = _placesRepository.GetAll();

      Assert.That(allplaces.Count() == 24800);
    }

    [Test]
    public void GetById_Returns_Matching_Place()
    {
      // arrange
      const int placeId = 2;

      // act
      var place = _placesRepository.GetById(placeId);

      // assert
      Assert.That(place.Name == "Ab Lench");
    }

    [Test]
    public void Can_Get_Places_By_Name()
    {
      var places = _placesRepository.GetAll();

      var placesID = places.FirstOrDefault(
        x => x.Name == "Leeds").PlacesID;

      Assert.That(placesID, Is.EqualTo(12472));
    }
  }
}
