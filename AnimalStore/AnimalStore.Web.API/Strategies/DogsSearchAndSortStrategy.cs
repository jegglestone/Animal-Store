using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Common.Constants;
using System.Device.Location;

namespace AnimalStore.Web.API.Strategies
{
    public interface IDogFilterStrategy
    {
        IQueryable<Dog> Filter(int breedId);
    }

    /// <summary>
    /// Strategy pattern to allow concrete implementations of various
    /// filtering patterns.
    /// </summary>
    public abstract class DogsSearchAndSortStrategy :IDogFilterStrategy
    {
        protected IRepository<Dog> _dogsRepository;

        private static class SortExpressions
        {
            public static Expression<Func<Dog, int>> PRICE_ORDER
            {
                get { return dog => dog.Price; }
            }

            public static Expression<Func<Dog, DateTime>> CREATED_ON_ORDER
            {
                get { return dog => dog.CreatedOn; }
            }
        }

        protected static IEnumerable<Dog> SortDogs(IQueryable<Dog> dogs, string sortBy = null)
        {
            IOrderedQueryable<Dog> orderedDogs;

            switch (sortBy)
            {
                case SearchSortOptions.PRICE_HIGHEST:
                    orderedDogs=dogs.OrderByDescending(SortExpressions.PRICE_ORDER);
                    break;
                case SearchSortOptions.PRICE_LOWEST:
                    orderedDogs=dogs.OrderBy(SortExpressions.PRICE_ORDER);
                    break;
                default:
                    orderedDogs=dogs.OrderByDescending(SortExpressions.CREATED_ON_ORDER);
                    break;
            }

            return orderedDogs;
        }

        public abstract IQueryable<Dog> Filter(int id);
    }

    public interface IDogBreedFilterStrategy : IDogFilterStrategy
    {
    }

    public sealed class DogBreedFilter : DogsSearchAndSortStrategy, IDogBreedFilterStrategy
    {
        public DogBreedFilter(IRepository<Dog> dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }

        public override IQueryable<Dog> Filter(int breedId)
        {
            var dogsUnsorted = _dogsRepository.GetAll()
                .Where(x => x.Breed.Id == breedId);

            return dogsUnsorted;
        }
    }

    public interface IDogCategoryFilterStrategy : IDogFilterStrategy
    {
        IQueryable<Dog> Filter(int categoryId, int breedId);
        IEnumerable<Dog> Sort(IQueryable<Dog> dogsUnsorted, string sortBy);
    }

    public sealed class DogCategoryFilter : DogsSearchAndSortStrategy, IDogCategoryFilterStrategy
    {
        public DogCategoryFilter(IRepository<Dog> dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }

        public override IQueryable<Dog> Filter(int categoryId)
        {
            var dogsUnsorted = _dogsRepository.GetAll()
                 .Where(x => x.Breed.Category.Id == categoryId);

            return dogsUnsorted;
        }

        public IQueryable<Dog> Filter(int categoryId, int breedToExcludeId)
        {
            var dogsUnsorted = _dogsRepository.GetAll()
                 .Where(x => x.Breed.Category.Id == categoryId && x.Breed.Id != breedToExcludeId);

            return dogsUnsorted;
        }

        public IEnumerable<Dog> Sort(IQueryable<Dog> dogsUnsorted, string sortBy)
        {
            var dogsOrdered = SortDogs(dogsUnsorted, sortBy);

            return dogsOrdered;
        }
    }

    public interface IDoglocationFilterStrategy
    {
        IEnumerable<Dog> Filter(IQueryable<Dog> dogs, int placeId);
        IEnumerable<Dog> Sort(IEnumerable<Dog> dogsUnsorted);
    }

    public sealed class DogLocationFilter : IDoglocationFilterStrategy
    {
        IRepository<Dog> _dogsRepository;
        IPlacesRepository _placesRepository;

        public DogLocationFilter(IRepository<Dog> dogsRepository, IPlacesRepository placesRepository)
        {
            _dogsRepository = dogsRepository;
            _placesRepository = placesRepository;
        }

        //TODO: hefty unit testing
        public IEnumerable<Dog> Filter(IQueryable<Dog> dogs, int placeId)
        {
            var originalPlace = _placesRepository.GetById(placeId);
            var originalPlaceGeoCode = new GeoCoordinate(originalPlace.Latitude, originalPlace.longitude);

            var allPlaces = _placesRepository.GetAll().ToList();  // Enumerate so we dont query multiple times later
            var dogsList = dogs.ToList<Dog>();
            List<Dog> dogsWithinRadius = new List<Dog>();

            foreach (var dog in dogsList)
            {
                var place = allPlaces.Where(x => x.Id == dog.PlaceId).Single(); // make sure queries collection not db
                var currentDogGeoCode = new GeoCoordinate(place.Latitude, place.longitude);
                var distance = originalPlaceGeoCode.GetDistanceTo(currentDogGeoCode);
                if (distance < 50000)   //TODO: Configurable number of metres
                {
                    dog.Distance = distance;
                    dogsWithinRadius.Add(dog);
                }
            }

            return dogsWithinRadius; 
        }

        public IEnumerable<Dog> Sort(IEnumerable<Dog> dogsUnsorted)
        {
            return dogsUnsorted.OrderBy(dog => dog.Distance);
        }
    }
}