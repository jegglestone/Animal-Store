using System.Collections.Generic;
using System.Linq;
using AnimalStore.Model;

namespace AnimalStore.Web.API.Strategies
{
    public interface IDogCategoryFilterStrategy : IDogFilterStrategy
    {
        IQueryable<Dog> Filter(int categoryId, int breedToExcludeId);
        IEnumerable<Dog> Sort(IQueryable<Dog> dogsUnsorted, string sortBy);
    }
}