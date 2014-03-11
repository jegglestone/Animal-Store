using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Web.API.Strategies;
using AnimalStore.Web.API.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalStore.Web.API.Helpers
{
    public class DogCategoryService : IDogCategoryService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<Breed> _breedsRepository;
        private readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;

        public DogCategoryService(IConfiguration configuration
            , IRepository<Breed> breedsRepository
            , IDogCategoryFilterStrategy dogCategoryFilterStrategy)
        {
            _configuration = configuration;
            _breedsRepository = breedsRepository;
            _dogCategoryFilterStrategy = dogCategoryFilterStrategy;
        }

        public IQueryable<Dog> AddDogsInSameCategoryToDogsCollection(IQueryable<Dog> matchingBreedDogs, int breedId)
        {
            IQueryable<Dog> dogs = null;
            IQueryable<Dog> dogsInSameCategory = null;

            if (matchingBreedDogs.Count() < _configuration.GetSearchResultsMinimumMatchingNumber())
            {
                dogsInSameCategory = GetDogsInSameCategory(breedId);
            }

            if (dogsInSameCategory != null && matchingBreedDogs != null)
            {
                dogs = matchingBreedDogs.Union(dogsInSameCategory);
            }
            else if (dogsInSameCategory == null && matchingBreedDogs != null)
            {
                dogs = matchingBreedDogs;
            }
            else if (dogsInSameCategory != null)
            {
                dogs = dogsInSameCategory;
            }

            return dogs;
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
    }
}