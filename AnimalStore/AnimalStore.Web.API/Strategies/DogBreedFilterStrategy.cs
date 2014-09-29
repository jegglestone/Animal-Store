using System.Linq;
using AnimalStore.Data.Repositories.Animals;
using AnimalStore.Model;

namespace AnimalStore.Web.API.Strategies
{
    public sealed class DogBreedFilterStrategy : DogsSearchAndSortStrategy, IDogBreedFilterStrategy
    {
        public DogBreedFilterStrategy(IRepository<Dog> dogsRepository)
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