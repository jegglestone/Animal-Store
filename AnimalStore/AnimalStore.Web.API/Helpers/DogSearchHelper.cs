using System;
using System.Collections.Generic;
using System.Linq;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Web.API.Strategies;
using AnimalStore.Web.API.Wrappers;

namespace AnimalStore.Web.API.Helpers
{
    public class DogSearchHelper : IDogSearchHelper
    {
        private readonly IDogBreedFilterStrategy _dogBreedFilterStrategy;
        private readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;
        private readonly IDogCategoryService _dogCategoryService;
        private readonly IDogLocationFilterStrategy _dogLocationFilterStrategy;
        private readonly IConfiguration _configuration;

        public DogSearchHelper(
            IDogBreedFilterStrategy dogBreedFilterStrategy
            , IDogCategoryFilterStrategy dogCategoryFilterStrategy
            , IDogCategoryService dogCategoryService
            , IDogLocationFilterStrategy dogLocationFilterStrategy
            , IConfiguration configuration)
        {
            _dogBreedFilterStrategy = dogBreedFilterStrategy;
            _dogCategoryFilterStrategy = dogCategoryFilterStrategy;
            _dogCategoryService = dogCategoryService;
            _dogLocationFilterStrategy = dogLocationFilterStrategy;
            _configuration = configuration;
        }

        public IEnumerable<Dog> GetDogsList(int breedId, string sortBy, int placeId = 0)
        {
            var matchingDogs = _dogBreedFilterStrategy.Filter(breedId);
            return matchingDogs;
        }

        public IEnumerable<Dog> ApplyDogLocationAndSortFiltering(IQueryable<Dog> matchingDogs, int breedId, string sortBy, int placeId = 0)
        {
            IQueryable<Dog> dogs = _dogCategoryService.AddDogsInSameCategoryToDogsCollection(matchingDogs, breedId);
            IEnumerable<Dog> dogsSorted;

            if (isLocationSearch(placeId))
            {
                dogsSorted = GetDogsInSameRegion(placeId, breedId, dogs);
                dogsSorted = _dogLocationFilterStrategy.Sort(dogsSorted);
            }
            else
            {
                dogsSorted = _dogCategoryFilterStrategy.Sort(dogs, sortBy);
            }

            return dogsSorted;
        }

        private IEnumerable<Dog> GetDogsInSameRegion(int placeId, int breedId, IQueryable<Dog> dogs)
        {
            // TODO: see if we have enough matching breeds to just return relevant ones
            var dogsResults = _dogLocationFilterStrategy.Filter(dogs, placeId);

            var dogsInSameRegion = dogsResults as IList<Dog> ?? dogsResults.ToList();
            return dogsInSameRegion.Count(x => x.BreedId == breedId) >= _configuration.GetSearchResultsMinimumMatchingNumber() 
                ? dogsInSameRegion.Where(x => x.BreedId == breedId) 
                : dogsInSameRegion;
        }

        private bool isLocationSearch(int placeId)
        {
            if (placeId != 0)
                return true;
            return false;
        }
    }
}