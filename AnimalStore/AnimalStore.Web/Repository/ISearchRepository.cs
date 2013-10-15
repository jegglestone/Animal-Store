using System.Collections.Generic;
using AnimalStore.Model;

namespace AnimalStore.Web.Repository
{
    public interface ISearchRepository
    {
        IList<Breed> GetBreeds();
        // Locations 
    }
}
