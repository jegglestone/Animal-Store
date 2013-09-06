using AnimalStore.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Data.UnitsOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        int Save();
        IContext Context { get; }
    }
}
