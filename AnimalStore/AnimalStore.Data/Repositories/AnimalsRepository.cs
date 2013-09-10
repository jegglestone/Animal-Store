using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;

namespace AnimalStore.Data.Repositories
{
    public class AnimalsRepository : GenericRepository<Animal>
    {
        public AnimalsRepository(IUnitOfWork unitOfWork) :
            base(unitOfWork)
        {
        }
    }
}
