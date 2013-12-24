using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Web.API.Models;
using AnimalStore.Web.API.Strategies;

namespace AnimalStore.Web.API.Helpers
{
    public class DogSearchHelper : IDogSearchHelper
    {
        private readonly IDogBreedFilterStrategy _dogBreedFilterStrategy;
        private readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;
        private readonly IRepository<Breed> _breedsRepository;
        private readonly int _minimumResults = int.Parse(ConfigurationManager.AppSettings[AppSettingKeys.SearchResultsMinimumMatchingNumber]);

        public DogSearchHelper(IDogBreedFilterStrategy dogBreedFilterStrategy, IDogCategoryFilterStrategy dogCategoryFilterStrategy, IRepository<Breed> breedsRepository)
        {
            _dogBreedFilterStrategy = dogBreedFilterStrategy;
            _dogCategoryFilterStrategy = dogCategoryFilterStrategy;
            _breedsRepository = breedsRepository;
        }

        //TODO: Unit test
        public IEnumerable<Dog> GetSortedDogsList(int breedId, string sortBy)
        {
            var matchingDogs = _dogBreedFilterStrategy.Filter(breedId);
            IQueryable<Dog> dogs = null;
            IQueryable<Dog> dogsInSameCategory = null;

            if (matchingDogs.Count() < _minimumResults)
            {
                dogsInSameCategory = GetDogsInSameCategory(breedId);
            }

            if (dogsInSameCategory != null && matchingDogs != null)
            {
                dogs = matchingDogs.Concat(dogsInSameCategory);
            }
            else if (dogsInSameCategory == null && matchingDogs != null)
            {
                dogs = matchingDogs;
            }
            else if (dogsInSameCategory != null)
            {
                dogs = dogsInSameCategory;
            }

            IEnumerable<Dog> dogsSorted = _dogCategoryFilterStrategy.Sort(dogs, sortBy);

            return dogsSorted;
        }

        private IQueryable<Dog> GetDogsInSameCategory(int breedId)
        {
            var categoryId = _breedsRepository.GetById(breedId).Category.Id;

            return _dogCategoryFilterStrategy.Filter(categoryId, breedId);
        }
    }
}