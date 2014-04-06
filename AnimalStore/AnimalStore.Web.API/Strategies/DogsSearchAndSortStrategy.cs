using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Common.Constants;

namespace AnimalStore.Web.API.Strategies
{
    /// <summary>
    /// Strategy pattern to allow concrete implementations of various
    /// filtering patterns.
    /// </summary>
    public abstract class DogsSearchAndSortStrategy :IDogFilterStrategy
    {
        protected IRepository<Dog> DogsRepository;

        private static class SortExpressions
        {
            public static Expression<Func<Dog, int>> PriceOrder
            {
                get { return dog => dog.Price; }
            }

            public static Expression<Func<Dog, DateTime>> CreatedOnOrder
            {
                get { return dog => dog.CreatedOn; }
            }
        }

        protected static IEnumerable<Dog> SortDogs(IQueryable<Dog> dogs, string sortBy = null)
        {
            IOrderedQueryable<Dog> orderedDogs;

            switch (sortBy)
            {
                case SearchSortOptions.PRICE_HIGHEST:
                    orderedDogs=dogs.OrderByDescending(SortExpressions.PriceOrder);
                    break;
                case SearchSortOptions.PRICE_LOWEST:
                    orderedDogs=dogs.OrderBy(SortExpressions.PriceOrder);
                    break;
                default:
                    orderedDogs=dogs.OrderByDescending(SortExpressions.CreatedOnOrder);
                    break;
            }

            return orderedDogs;
        }

        public abstract IQueryable<Dog> Filter(int id);
    }
}