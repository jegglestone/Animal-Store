using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Data.DataContext
{
    public interface IContext : IDisposable
    {
        int SaveChanges();
        void SetModified(object entity);
        void SetAdd(object entity);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry(object entity);
    }
}
