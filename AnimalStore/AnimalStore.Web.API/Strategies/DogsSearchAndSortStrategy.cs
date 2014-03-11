using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AnimalStore.Data.Repositories;
using AnimalStore.Data.Repositories.Places;
using AnimalStore.Model;
using AnimalStore.Common.Constants;
using System.Device.Location;
using AnimalStore.Web.API.Wrappers;
using System.Data.Entity;

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
        protected IRepository<Dog> DogsRepository;

        private static class SortExpressions
        {
            public static Expression<Func<Dog, int>> PriceOrder
            {
                get { return dog => dog.Price; }
            }

            public static Expression<Func<Dog, DateTime>> CreatedOnOrder
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
                    orderedDogs=dogs.OrderByDescending(SortExpressions.PriceOrder);
                    break;
                case SearchSortOptions.PRICE_LOWEST:
                    orderedDogs=dogs.OrderBy(SortExpressions.PriceOrder);
                    break;
                default:
                    orderedDogs=dogs.OrderByDescending(SortExpressions.CreatedOnOrder);
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
            DogsRepository = dogsRepository;
        }

        public override IQueryable<Dog> Filter(int breedId)
        {
            var dogsUnsorted = DogsRepository.GetAll()
                .Where(x => x.Breed.Id == breedId);

            return dogsUnsorted;
        }
    }

    public interface IDogCategoryFilterStrategy : IDogFilterStrategy
    {
        IQueryable<Dog> Filter(int categoryId, int breedToExcludeId);
        IEnumerable<Dog> Sort(IQueryable<Dog> dogsUnsorted, string sortBy);
    }

    public sealed class DogCategoryFilter : DogsSearchAndSortStrategy, IDogCategoryFilterStrategy
    {
        public DogCategoryFilter(IRepository<Dog> dogsRepository)
        {
            DogsRepository = dogsRepository;
        }

        public override IQueryable<Dog> Filter(int categoryId)
        {
            var dogsUnsorted = DogsRepository.GetAll()
                 .Where(x => x.Breed.Category.Id == categoryId);

            return dogsUnsorted;
        }

        public IQueryable<Dog> Filter(int categoryId, int breedToExcludeId)
        {
            var dogsUnsorted = DogsRepository.GetAll()
                 .Where(x => x.Breed.Category.Id == categoryId 
                     && x.Breed.Id != breedToExcludeId
                     ).Include("Breed");

            return dogsUnsorted;
        }

        public IEnumerable<Dog> Sort(IQueryable<Dog> dogsUnsorted, string sortBy)
        {
            var dogsOrdered = SortDogs(dogsUnsorted, sortBy);

            return dogsOrdered;
        }
    }

    public interface IDogLocationFilterStrategy
    {
        IEnumerable<Dog> Filter(IQueryable<Dog> dogs, int placeId);
        IEnumerable<Dog> Sort(IEnumerable<Dog> dogsUnsorted);
    }

    public sealed class DogLocationFilter : IDogLocationFilterStrategy
    {
        readonly IPlacesRepository _placesRepository;
        readonly IConfiguration _configuration;

        public DogLocationFilter(IPlacesRepository placesRepository, IConfiguration configuration)
        {
            _placesRepository = placesRepository;
            _configuration = configuration;
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
                var place = allPlaces.Single(x => x.Id == dog.PlaceId); // make sure queries collection not db
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

        public IEnumerable<Dog> Sort(IEnumerable<Dog> dogsUnsorted)
        {
            return dogsUnsorted.OrderBy(dog => dog.Distance);
        }
    }
}