using System.Data.Entity;
using AnimalStore.Model;
using System;

namespace AnimalStore.Data.DataContext
{
    public interface IAnimalsDataContext : IContext
    {
        IDbSet<Animal> Animals { get; }
        IDbSet<Species> Species { get; }
        IDbSet<Breed> Breeds { get; }

    }
}
