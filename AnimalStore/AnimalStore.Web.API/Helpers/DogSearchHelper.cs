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
        private readonly IDogLocationFilterStrategy _dogLocationFilterStrategy;
        private readonly IRepository<Breed> _breedsRepository;
        private readonly IConfiguration _configuration;

        public DogSearchHelper(IDogBreedFilterStrategy dogBreedFilterStrategy, IDogCategoryFilterStrategy dogCategoryFilterStrategy, IDogLocationFilterStrategy dogLocationFilterStrategy, IRepository<Breed> breedsRepository, IConfiguration configuration)
        {
            _dogBreedFilterStrategy = dogBreedFilterStrategy;
            _dogCategoryFilterStrategy = dogCategoryFilterStrategy;
            _dogLocationFilterStrategy = dogLocationFilterStrategy;
            _breedsRepository = breedsRepository;
            _configuration = configuration;
        }

        public IEnumerable<Dog> GetDogsList(int breedId, string sortBy, int placeId = 0)
        {
            var matchingDogs = _dogBreedFilterStrategy.Filter(breedId);
            return matchingDogs;
        }

        public IEnumerable<Dog> ApplyDogLocationAndSortFiltering(IQueryable<Dog> matchingDogs, int breedId, string sortBy, int placeId = 0)
        {
            IQueryable<Dog> dogs = AddDogsInSameCategoryToDogsCollection(matchingDogs, breedId);

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

        public IQueryable<Dog> AddDogsInSameCategoryToDogsCollection(IQueryable<Dog> matchingDogs, int breedId)
        {
            IQueryable<Dog> dogs = null;
            IQueryable<Dog> dogsInSameCategory = null;

            if (matchingDogs.Count() < _configuration.GetSearchResultsMinimumMatchingNumber())
            {
                dogsInSameCategory = GetDogsInSameCategory(breedId);
            }

            if (dogsInSameCategory != null && matchingDogs != null)
            {
                dogs = matchingDogs.Union(dogsInSameCategory);
            }
            else if (dogsInSameCategory == null && matchingDogs != null)
            {
                dogs = matchingDogs;
            }
            else if (dogsInSameCategory != null)
            {
                dogs = dogsInSameCategory;
            }

            return dogs;
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

        private IQueryable<Dog> GetDogsInSameCategory(int breedId)
        {
            int categoryId;
            try
            {
                categoryId = _breedsRepository.GetById(breedId).Category.Id;
            }
            catch (NullReferenceException)
            {
                return null;
            }

            return _dogCategoryFilterStrategy.Filter(categoryId, breedId);
        }

        private bool isLocationSearch(int placeId)
        {
            if (placeId != 0)
                return true;
            return false;
        }
    }
}