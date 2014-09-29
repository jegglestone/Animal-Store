namespace AnimalStore.Web.API.Services
{
  using System.Linq;

  public interface IDogCategoryService
    {
        IQueryable<Model.Dog> AddDogsInSameCategoryToDogsCollection(IQueryable<AnimalStore.Model.Dog> matchingBreedDogs, int breedId);
    }
}
