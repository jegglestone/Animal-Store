namespace AnimalStore.Web.API.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Model;

  public interface IDogSearchService
    {
        IEnumerable<Dog> GetDogsByBreed(int breedId);

        IEnumerable<Dog> ApplyDogLocationFilteringAndSorting(IQueryable<Dog> matchingDogs, int breedId, string sortBy, int placeId = 0);
    }
}