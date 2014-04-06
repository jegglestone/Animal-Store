using AnimalStore.Model;
using System.Collections.Generic;

namespace AnimalStore.Data.Repositories.Places
{
    public interface IPlacesRepository
    {
        IEnumerable<Place> GetAll();
        Place GetById(int id);
    }
}
