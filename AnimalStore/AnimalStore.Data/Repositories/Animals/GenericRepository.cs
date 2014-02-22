using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AnimalStore.Data.DataContext;
using AnimalStore.Data.UnitsOfWork;

namespace AnimalStore.Data.Repositories.Animals
{
    public abstract class GenericRepository<T> : IRepository<T> 
        where T : class
    {
        private IDbSet<T> DbSet {get; set;}
        public IContext Context { get; set; }

        protected GenericRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null || unitOfWork.Context == null)
                throw new ArgumentNullException(
                    "unitOfWork", "An instance of UnitOfWork with a DbContext is required to use this generic repository");

            Context = unitOfWork.Context;
            DbSet = Context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
                entry.State = EntityState.Added;
            else
                DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
                DbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
                entry.State = EntityState.Deleted;
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = DbSet.Find(id);

            if (entity != null)
                Delete(entity);
        }

        public void Detach(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public void Dispose()
        {
        }
    }
}
