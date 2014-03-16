using System.Collections.Generic;
using AnimalStore.Model;
using System.Linq;

namespace AnimalStore.Web.API.Helpers
{
    public interface IDogSearchManager
    {
        IEnumerable<Dog> GetDogsByBreed(int breedId);

        IEnumerable<Dog> ApplyDogLocationFilteringAndSorting(IQueryable<Dog> matchingDogs, int breedId, string sortBy, int placeId = 0);
    }
}