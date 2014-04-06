using System.Collections.Generic;
using System.Linq;
using AnimalStore.Model;

namespace AnimalStore.Web.API.Utilities
{
    public interface IDogSearchManager
    {
        IEnumerable<Dog> GetDogsByBreed(int breedId);

        IEnumerable<Dog> ApplyDogLocationFilteringAndSorting(IQueryable<Dog> matchingDogs, int breedId, string sortBy, int placeId = 0);
    }
}