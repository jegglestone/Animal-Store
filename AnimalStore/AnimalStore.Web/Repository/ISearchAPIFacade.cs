using System.Collections.Generic;
using AnimalStore.Model;

namespace AnimalStore.Web.Repository
{
    public interface ISearchAPIFacade
    {
        IList<Breed> GetBreeds();
        PageableResults<Dog> GetDogs(int page, int pageSize);
        PageableResults<Dog> GetDogs(int page, int pageSize, int breedId, string breedName, string sortBy=null);
    }
}
