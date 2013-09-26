using System.Data.Entity;
using AnimalStore.Model;

namespace AnimalStore.Data.DataContext
{
    public interface IAnimalsDataContext : IContext
    {
        IDbSet<Animal> Animals { get; }
        IDbSet<Species> Species { get; }
        IDbSet<Breed> Breeds { get; }
    }
}
