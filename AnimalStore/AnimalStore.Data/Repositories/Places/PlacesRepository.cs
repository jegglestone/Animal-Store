using AnimalStore.Data.DataContext;
using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Data.Repositories
{
    public interface IPlacesRepository
    {
        IQueryable<Place> GetAll();

        Place GetById(int id);
    }
    public class PlacesRepository: IPlacesRepository
    {
        private IDbSet<Place> DBSet { get; set; }
        public DbContext Context { get; set; }

        public PlacesRepository()
        {
            Context = new PlacesDataContext();
            DBSet = Context.Set<Place>();
        }
        public IQueryable<Place> GetAll()
        {
            return DBSet;
        }

        public Place GetById(int id)
        {
            return DBSet.Find(id);
        }
    }
}
