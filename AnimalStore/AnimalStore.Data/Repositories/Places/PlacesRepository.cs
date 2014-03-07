using System.Data.Entity;
using System.Linq;
using AnimalStore.Data.DataContext;
using AnimalStore.Model;

namespace AnimalStore.Data.Repositories.Places
{
    //TODO: Seperate file
    public interface IPlacesRepository
    {
        IQueryable<Place> GetAll();

        Place GetById(int id);
    }
    public class PlacesRepository: IPlacesRepository
    {
        private IDbSet<Place> DbSet { get; set; }
        public DbContext Context { get; set; }

        public PlacesRepository()
        {
            Context = new PlacesDataContext();
            DbSet = Context.Set<Place>();
        }
        public IQueryable<Place> GetAll()
        {
            return DbSet;
        }

        public Place GetById(int id)
        {
            return DbSet.Find(id);
        }
    }
}
