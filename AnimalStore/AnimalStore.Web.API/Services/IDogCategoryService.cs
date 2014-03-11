using System;
using System.Linq;

namespace AnimalStore.Web.API.Helpers
{
    public interface IDogCategoryService
    {
        IQueryable<AnimalStore.Model.Dog> AddDogsInSameCategoryToDogsCollection(IQueryable<AnimalStore.Model.Dog> matchingBreedDogs, int breedId);
    }
}
