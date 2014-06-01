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

        public IEnumerable<Dog> Filter(IQueryable<Dog> dogs, int placeId)
        {
            var originalPlace = _placesRepository.GetById(placeId);
            var originalPlaceGeoCode = new GeoCoordinate(originalPlace.Latitude, originalPlace.Longitude);

            var allPlaces = _placesRepository.GetAll().ToList(); 
            var dogsList = dogs.ToList();
            var dogsWithinRadius = new List<Dog>();

            foreach (var dog in dogsList)
            {
                var place = allPlaces.Single(x => int.Parse(x.PlacesID.ToString()) == dog.PlaceId); 
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