using AnimalStore.Model;
using System.Data.Entity;

namespace AnimalStore.Data.Repositories
{
    public class AnimalsRepository : GenericRepository<Animal>
    {
        public AnimalsRepository(DbContext context) : 
            base(context)
        { }
    }
}
