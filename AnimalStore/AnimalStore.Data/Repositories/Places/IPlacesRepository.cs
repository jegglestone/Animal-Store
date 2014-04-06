using AnimalStore.Model;
using System.Collections.Generic;

namespace AnimalStore.Data.Repositories.Places
{
    interface IPlacesRepository
    {
        IEnumerable<Place> GetAll();
        Place GetById(int id);
    }
}
