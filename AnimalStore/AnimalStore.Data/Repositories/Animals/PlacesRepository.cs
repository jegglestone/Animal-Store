using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;

namespace AnimalStore.Data.Repositories.Animals
{
    public class PlacesRepository : GenericRepository<Place>
    {
        public PlacesRepository(IUnitOfWork unitOfWork) : 
            base(unitOfWork)
        {
        }
    }
}
