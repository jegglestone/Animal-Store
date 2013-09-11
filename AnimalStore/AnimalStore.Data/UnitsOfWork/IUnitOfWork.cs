using AnimalStore.Data.DataContext;
using System;

namespace AnimalStore.Data.UnitsOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        int Save();
        IContext Context { get; }
    }
}
