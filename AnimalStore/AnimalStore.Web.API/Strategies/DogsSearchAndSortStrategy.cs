using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;

namespace AnimalStore.Web.API.Strategies
{
    public abstract class DogsSearchAndSortStrategy
    {
        protected IRepository<Dog> _dogsRepository;

        public abstract IEnumerable<Dog> Filter(int id, Expression<Func<Dog, DateTime>> orderBy);
    }

    public interface IDogBreedFilterStrategy
    {
        IEnumerable<Dog> Filter(int breedId, Expression<Func<Dog, DateTime>> orderBy);
    }

    public sealed class DogBreedFilter : DogsSearchAndSortStrategy, IDogBreedFilterStrategy
    {
        public DogBreedFilter(IRepository<Dog> dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }

        public override IEnumerable<Dog> Filter(int breedId, Expression<Func<Dog, DateTime>> orderBy)
        {
            return _dogsRepository.GetAll()
                .Where(x => x.Breed.Id == breedId)
                .OrderByDescending(orderBy).AsEnumerable();
        }
    }

    public interface IDogCategoryFilterStrategy
    {
        IEnumerable<Dog> Filter(int categoryId, Expression<Func<Dog, DateTime>> orderBy);
    }

    public sealed class DogCategoryFilter : DogsSearchAndSortStrategy, IDogCategoryFilterStrategy
    {
        public DogCategoryFilter(IRepository<Dog> dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }

        public override IEnumerable<Dog> Filter(int categoryId, Expression<Func<Dog, DateTime>> orderBy)
        {
            return _dogsRepository.GetAll()
                                  .Where(x => x.Breed.Category.Id == categoryId)
                                  .OrderBy(orderBy).AsEnumerable();
        }
    }
}