using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;

namespace AnimalStore.Web.API.Strategies
{
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
}