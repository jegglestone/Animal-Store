﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Common.Constants;

namespace AnimalStore.Web.API.Strategies
{
    public abstract class DogsSearchAndSortStrategy
    {
        protected IRepository<Dog> _dogsRepository;

        public static class SortExpressions
        {
            public static Expression<Func<Dog, int>> PRICE_ORDER
            {
                get { return dog => dog.Price; }
            }

            public static Expression<Func<Dog, DateTime>> CREATED_ON_ORDER
            {
                get { return dog => dog.CreatedOn; }
            }
        }

        public IOrderedQueryable<Dog> SortDogs(IQueryable<Dog> dogs, string sortBy = null)
        {
            IOrderedQueryable<Dog> orderedDogs;
            switch (sortBy)
            {
                case SearchSortOptions.PRICE_HIGHEST:
                    orderedDogs=dogs.OrderByDescending(SortExpressions.PRICE_ORDER);
                    break;
                case SearchSortOptions.PRICE_LOWEST:
                    orderedDogs=dogs.OrderBy(SortExpressions.PRICE_ORDER);
                    break;
                default:
                    orderedDogs=dogs.OrderByDescending(SortExpressions.CREATED_ON_ORDER);
                    break;
            }

            return orderedDogs;
        }

        public abstract IEnumerable<Dog> Filter(int id, string sortBy = null);
    }


    public interface IDogBreedFilterStrategy
    {
        IEnumerable<Dog> Filter(int breedId, string sortBy = null);
    }

    public sealed class DogBreedFilter : DogsSearchAndSortStrategy, IDogBreedFilterStrategy
    {
        public DogBreedFilter(IRepository<Dog> dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }

        public override IEnumerable<Dog> Filter(int breedId, string sortBy = null)
        {
            var dogsUnsorted = _dogsRepository.GetAll()
                .Where(x => x.Breed.Id == breedId);

            var dogsOrdered = SortDogs(dogsUnsorted, sortBy);

            return dogsOrdered.AsEnumerable();
        }
    }

    public interface IDogCategoryFilterStrategy
    {
        IEnumerable<Dog> Filter(int categoryId, string sortBy);
    }

    public sealed class DogCategoryFilter : DogsSearchAndSortStrategy, IDogCategoryFilterStrategy
    {
        public DogCategoryFilter(IRepository<Dog> dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }

        public override IEnumerable<Dog> Filter(int categoryId, string sortBy = null)
        {
            var dogsUnsorted = _dogsRepository.GetAll()
                 .Where(x => x.Breed.Category.Id == categoryId);

            var dogsOrdered = SortDogs(dogsUnsorted, sortBy);

            return dogsOrdered.AsEnumerable();
        }
    }
}