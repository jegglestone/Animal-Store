using System.Collections.Generic;
using AnimalStore.Model;
using System.Linq;

namespace AnimalStore.Web.API.Helpers
{
    public interface IDogSearchHelper
    {
        IEnumerable<Dog> GetDogsList(int breedId, string sortBy, int placeId=0);

        IQueryable<Dog> AddDogsInSameCategoryToDogsCollection(IQueryable<Dog> matchingDogs, int breedId);

        IEnumerable<Dog> ApplyDogLocationAndSortFiltering(IQueryable<Dog> matchingDogs, int breedId, string sortBy, int placeId = 0);
    }
}