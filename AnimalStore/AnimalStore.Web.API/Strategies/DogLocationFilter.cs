using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using AnimalStore.Data.Repositories.Places;
using AnimalStore.Model;
using AnimalStore.Web.API.Wrappers;

namespace AnimalStore.Web.API.Strategies
{
    public sealed class DogLocationFilter : IDogLocationFilterStrategy
    {
        readonly IPlacesRepository _placesRepository;
        readonly IConfiguration _configuration;

        public DogLocationFilter(IPlacesRepository placesRepository, IConfiguration configuration)
        {
            _placesRepository = placesRepository;
            _configuration = configuration;
        }

        public IEnumerable<Dog> Sort(IEnumerable<Dog> dogsUnsorted)
        {
            return dogsUnsorted.OrderBy(dog => dog.Distance);
        }

        //TODO: hefty unit testing
        public IEnumerable<Dog> Filter(IQueryable<Dog> dogs, int placeId)
        {
            var originalPlace = _placesRepository.GetById(placeId);
            var originalPlaceGeoCode = new GeoCoordinate(originalPlace.Latitude, originalPlace.Longitude);

            var allPlaces = _placesRepository.GetAll().ToList(); //TODO: if less than 10 records it'd be more efficient to simply query the db 10 times
            // if more than 10 we will load all 24000 places to memory (with ToList()) to reduce number of queries
            // this will involve splitting this method in two
            var dogsList = dogs.ToList();
            var dogsWithinRadius = new List<Dog>();

            foreach (var dog in dogsList)
            {
                var place = allPlaces.Single(x => int.Parse(x.PlacesID.ToString()) == dog.PlaceId); // make sure queries collection not db
                var currentDogGeoCode = new GeoCoordinate(place.Latitude, place.Longitude);
                var distance = originalPlaceGeoCode.GetDistanceTo(currentDogGeoCode);
                if (distance < _configuration.GetSearchRadiusDefaultDistanceInMetres())
                {
                    dog.Distance = distance;
                    dogsWithinRadius.Add(dog);
                }
            }

            return dogsWithinRadius; 
        }

    }
}