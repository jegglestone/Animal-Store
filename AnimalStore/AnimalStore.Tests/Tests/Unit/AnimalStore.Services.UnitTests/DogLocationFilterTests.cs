using System.Collections.Generic;
using System.Linq;
using AnimalStore.Common.Configuration;
using AnimalStore.Data.Repositories.Places;
using AnimalStore.Model;
using AnimalStore.Web.API.Strategies;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnimalStore.Services.UnitTests
{
    [TestFixture]
    public class DogLocationFilterTests
    {
        [Test]
        public void DogLocationFilter_Returns_Items_Within_A_Configured_Radius_Of_A_Place()
        {
            var placesRepository = MockRepository.GenerateMock<IPlacesRepository>();
            var configuration = MockRepository.GenerateMock<IConfiguration>();

            configuration.Stub(x => x.GetSearchRadiusDefaultDistanceInMetres()).Return(50000);

            var sunderland = new Place()
            {
                PlacesID = 20629,
                Name = "Sunderland",
                County = "Tyne and Wear",
                Country = "England",
                Postcode = "DH7 7",
                Longitude = -1.384,
                Latitude = 54.907
            };
            var durhamNearby = new Place()
            {
                PlacesID = 15770,
                Name = "Aldin Grange",
                County = "Durham",
                Country = "England",
                Postcode = "CV6 6",
                Longitude = -1.624,
                Latitude = 54.781,
            };
            var midlandsFarAway = new Place()
            {
                PlacesID = 12864,
                Name = "Little Heath",
                County = "West Midlands",
                Country = "England",
                Postcode = "CV6 6",
                Longitude = -1.492,
                Latitude = 52.443
            };

            placesRepository.Stub(x => x.GetAll()).Return(
                new List<Place>()
                {
                    sunderland,
                    durhamNearby,                    
                    midlandsFarAway,
                }
            );
            placesRepository.Stub(x => x.GetById(sunderland.PlacesID)).Return(sunderland);

            var dogs = new DogSearchResultsListBuilder().ListOf3DogsWithConfigurableLocation(
                1, sunderland.PlacesID, durhamNearby.PlacesID, midlandsFarAway.PlacesID
                ).Build();

            var dogLocationFilter = new DogLocationFilterStrategy(placesRepository, configuration);

            // act
            var filteredDogs = dogLocationFilter.Filter(dogs.AsQueryable(), sunderland.PlacesID).ToList();

            // assert
            Assert.That(filteredDogs.Exists(dog => dog.PlaceId == sunderland.PlacesID));
            Assert.That(filteredDogs.Exists(dog => dog.PlaceId == durhamNearby.PlacesID));
            Assert.That(!filteredDogs.Exists(dog => dog.PlaceId == midlandsFarAway.PlacesID));
        }
    }
}
