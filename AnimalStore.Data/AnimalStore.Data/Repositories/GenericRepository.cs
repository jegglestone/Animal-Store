using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> 
        where T : class
    {
        protected DbSet<T> DBSet {get; set;}
        protected DbContext Context { get; set; }

        public GenericRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(
                    "An instance of DbContext is required to use this generic repository", "Null DbContext");

            this.Context = context;
            this.DBSet = this.Context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return this.DBSet;
        }

        public T GetById(int id)
        {
            return this.DBSet.Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
                entry.State = EntityState.Added;
            else
                this.DBSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
                this.DBSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
                entry.State = EntityState.Deleted;
            else
            {
                this.DBSet.Attach(entity);
                this.DBSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = this.DBSet.Find(id);

            if (entity != null)
                this.Delete(entity);
        }

        public void Detach(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            entry.State = EntityState.Detached;
        }
    }
}
