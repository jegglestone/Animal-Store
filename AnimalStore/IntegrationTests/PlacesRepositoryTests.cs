using AnimalStore.Data.Repositories.Places;
using MongoDB.Driver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [TestFixture]
    public class PlacesRepositoryTests
    {
        PlacesRepository _placesRepository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            MongoClient mongoClient = new MongoClient("Server=localhost:27017");

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
    }
}
