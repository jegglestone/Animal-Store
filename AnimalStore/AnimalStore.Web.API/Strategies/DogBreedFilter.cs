using System.Linq;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;

namespace AnimalStore.Web.API.Strategies
{
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
}