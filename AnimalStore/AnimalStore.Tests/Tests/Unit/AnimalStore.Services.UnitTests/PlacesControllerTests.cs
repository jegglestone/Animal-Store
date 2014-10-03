using System.Collections.Generic;
using AnimalStore.Data.Repositories.Places;
using AnimalStore.Model;
using AnimalStore.Web.API.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnimalStore.Services.UnitTests
{
  using System.Linq;

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
      List<Place> places = 
        placesController.GetByLocation(placeName).ToList();

      // assert
      Assert.That(places.First().PlacesID == expectedPlaceId);
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
      List<Place> places =
        placesController.GetByLocation(altPlaceName).ToList();

      // assert
      Assert.That(places.First().PlacesID == expectedPlaceId);
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
      List<Place> places =
        placesController.GetByLocation(county).ToList();

      // assert
      Assert.That(places.First().PlacesID == expectedPlaceId);
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
      List<Place> places =
        placesController.GetByLocation(postcode).ToList();

      // assert
      Assert.That(places.First().PlacesID == expectedPlaceId);
    }

    [TestCase("walker", 1)]
    [TestCase("horsforth", 2)]
    public void Get_WhenGivenALocationString_InLowerCase_Returns_LocationId(
      string lowerCasePlaceName, int expectedPlaceId)
    {
      // arrange
      var placesRepository = PlacesRepository();

      var placesController = new PlacesController(
        placesRepository);

      // act
      List<Place> places =
        placesController.GetByLocation(lowerCasePlaceName).ToList();

      // assert
      Assert.That(places.First().PlacesID == expectedPlaceId);
    }

    [Test]
    public void Get_WhenTwoMatchingPlaces_Returns_Both()
    {
      var placesRepository = MockRepository.GenerateMock<IPlacesRepository>();
      placesRepository.Stub(x => x.GetAll()).Return(
        new List<Place>
        {
          new Place
          {
            PlacesID = 1, 
            Name = "Walker", 
            County = "North Tyneside", 
            Postcode = "LE14 3"
          },
          new Place
          {
            PlacesID = 2, 
            Name = "Middleton", 
            County = "Northamptonshire", 
            Postcode = "LE16 8"
          },
        });

      var placesController = new PlacesController(
        placesRepository);

      // act
      var places =
        placesController.GetByLocation("LE1").ToList();

      // assert
      Assert.That(places.Count == 2);
    }

    [Test]
    public void Get_WhenMatchingCounty_Returns_AllTownsWithinCounty()
    {
      const string County = "Northamptonshire";

      var placesRepository = MockRepository.GenerateMock<IPlacesRepository>();
      placesRepository.Stub(x => x.GetAll()).Return(
        new List<Place>
        {
          new Place
          {
            PlacesID = 1, 
            Name = "Achurch", 
            County = County, 
            Postcode = "PE8 5"
          },
          new Place
          {
            PlacesID = 2, 
            Name = "Abthorpe", 
            County = County, 
            Postcode = "NN12 8"
          },
        });

      var placesController = new PlacesController(
        placesRepository);

      // act
      var places =
        placesController.GetByLocation(County).ToList();

      // assert
      Assert.That(places.Count == 2);
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
