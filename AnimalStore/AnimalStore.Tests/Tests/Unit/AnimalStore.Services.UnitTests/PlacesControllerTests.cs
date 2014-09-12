using System.Collections.Generic;
using AnimalStore.Data.Repositories.Places;
using AnimalStore.Model;
using AnimalStore.Web.API.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnimalStore.Services.UnitTests
{
  [TestFixture]
  public class PlacesControllerTests
  {
    [TestCase("Walker", 1)]
    [TestCase("Horsforth", 2)]
    public void Get_WhenGivenALocationString_Returns_LocationId(
      string placeName, int expectedPlaceId)
    {
      // arrange
      var placesRepository = PlacesRepository();

      var placesController= new PlacesController(
        placesRepository);

      // act
      int place = placesController.GetByLocation(placeName);

      // assert
      Assert.That(place == expectedPlaceId);
    }

    [TestCase("Chillingham", 1)]
    [TestCase("Cookridge", 2)]
    public void Get_WhenGivenLocationStringMatchingAltName_Returns_LocationId(
      string altPlaceName, int expectedPlaceId)
    {
      // arrange
      var placesRepository = PlacesRepository();

      var placesController = new PlacesController(
        placesRepository);

      // act
      int place = placesController.GetByLocation(altPlaceName);

      // assert
      Assert.That(place == expectedPlaceId);
    }

    [TestCase("North Tyneside", 1)]
    [TestCase("West Yorkshire", 2)]
    public void Get_WhenGivenLocationStringMatchingCounty_Returns_LocationId(
      string county, int expectedPlaceId)
    {
      // arrange
      var placesRepository = PlacesRepository();

      var placesController = new PlacesController(
        placesRepository);

      // act
      int place = placesController.GetByLocation(county);

      // assert
      Assert.That(place == expectedPlaceId);
    }

    [TestCase("NE28 7XX", 1)]
    [TestCase("LS16 7XX", 2)]
    public void Get_WhenGivenLocationStringMatchingPostcode_Returns_LocationId(
      string postcode, int expectedPlaceId)
    {
      // arrange
      var placesRepository = PlacesRepository();

      var placesController = new PlacesController(
        placesRepository);

      // act
      int place = placesController.GetByLocation(postcode);

      // assert
      Assert.That(place == expectedPlaceId);
    }

    private static IPlacesRepository PlacesRepository()
    {
      var placesRepository = MockRepository.GenerateMock<IPlacesRepository>();
      placesRepository.Stub(x => x.GetAll()).Return(
        new List<Place>
        {
          new Place
          {
            PlacesID = 1, 
            Name = "Walker", 
            AltName="Chillingham", 
            County = 
            "North Tyneside", 
            Postcode = "NE28 7XX"
          },
          new Place
          {
            PlacesID = 2, 
            Name = "Horsforth", 
            AltName="Cookridge", 
            County = "West Yorkshire", 
            Postcode = "LS16 7XX"
          },
        });
      return placesRepository;
    }
  }
}
