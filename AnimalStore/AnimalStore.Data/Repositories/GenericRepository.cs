using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AnimalStore.Data.DataContext;
using AnimalStore.Data.UnitsOfWork;

namespace AnimalStore.Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> 
        where T : class
    {
        protected IDbSet<T> DBSet {get; set;}
        public IContext Context { get; set; }

        protected GenericRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null || unitOfWork.Context == null)
                throw new ArgumentNullException(
                    "unitOfWork", "An instance of UnitOfWork with a DbContext is required to use this generic repository");

            Context = unitOfWork.Context;
            DBSet = Context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return DBSet;
        }

        public T GetById(int id)
        {
            return DBSet.Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
                entry.State = EntityState.Added;
            else
                DBSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
                DBSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
                entry.State = EntityState.Deleted;
            else
            {
                DBSet.Attach(entity);
                DBSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = DBSet.Find(id);

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
