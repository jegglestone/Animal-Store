using System.Collections.Generic;
using System.Linq;
using AnimalStore.Model;

namespace AnimalStore.Web.API.Strategies
{
    public interface IDogLocationFilterStrategy
    {
        IEnumerable<Dog> Filter(IQueryable<Dog> dogs, int placeId);
        IEnumerable<Dog> Sort(IEnumerable<Dog> dogsUnsorted);
    }
}