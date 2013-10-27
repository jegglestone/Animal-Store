using System.Collections.Generic;
using AnimalStore.Model;

namespace AnimalStore.Web.Repository
{
    public interface ISearchRepository
    {
        List<Breed> GetBreeds();
        // Locations 
    }
}
