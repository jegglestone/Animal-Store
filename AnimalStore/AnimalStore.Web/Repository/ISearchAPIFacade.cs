using System.Collections.Generic;
using AnimalStore.Model;

namespace AnimalStore.Web.Repository
{
  public interface ISearchAPIFacade
  {
    IList<Breed> GetBreeds();
    PageableResults<Dog> GetDogs(int page, int pageSize);
    PageableResults<Dog> GetDogsByBreed(int page, int pageSize, int breedId, string sortBy = null);
    PageableResults<Dog> GetDogsByLocation(int page, int pageSize, string location, string sortBy = null);
    PageableResults<Dog> GetDogsByBreedAndLocation(int breedId, int page, int pageSize, string location, string sortBy = null);
    Dog GetDogDetails(int id);
  }
}
