using AnimalStore.Data.DataContext;
using AnimalStore.Model;

namespace AnimalStore.Data.Repositories
{
    public class AnimalsRepository : GenericRepository<Animal>
    {
        public AnimalsRepository(IDataContext context) : 
            base(context)
        { }
    }
}
