using System.Collections.Generic;
using System.Linq;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Web.API.Strategies;

namespace AnimalStore.Web.API.Helpers
{
    public class DogSearchHelper : IDogSearchHelper
    {
        private readonly IDogBreedFilterStrategy _dogBreedFilterStrategy;
        private readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;
        private readonly IRepository<Breed> _breedsRepository;

        public DogSearchHelper(IDogBreedFilterStrategy dogBreedFilterStrategy, IDogCategoryFilterStrategy dogCategoryFilterStrategy, IRepository<Breed> breedsRepository)
        {
            _dogBreedFilterStrategy = dogBreedFilterStrategy;
            _dogCategoryFilterStrategy = dogCategoryFilterStrategy;
            _breedsRepository = breedsRepository;
        }

        //TODO: Helper class that can easily be mocked and stubbed
        public IEnumerable<Dog> GetSortedDogsList(int breedId, string sortBy)
        {
            //TODO: Get this into helper class
            var matchingDogs = _dogBreedFilterStrategy.Filter(breedId);

            var categoryId = _breedsRepository.GetById(breedId).Category.Id;

            var dogsInSameCategory = _dogCategoryFilterStrategy.Filter(categoryId, breedId);

            IQueryable<Dog> dogs = null;

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
    }
}