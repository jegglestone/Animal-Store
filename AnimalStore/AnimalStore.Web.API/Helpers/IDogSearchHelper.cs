using System.Collections.Generic;
using AnimalStore.Model;

namespace AnimalStore.Web.API.Helpers
{
    public interface IDogSearchHelper
    {
        IEnumerable<Dog> GetSortedDogsList(int breedId, string sortBy, int placeId=0);
    }
}