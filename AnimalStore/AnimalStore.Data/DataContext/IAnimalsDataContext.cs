using System.Data.Entity;
using AnimalStore.Model;

namespace AnimalStore.Data.DataContext
{
    public interface IAnimalsDataContext : IContext
    {
        IDbSet<Animal> Animals { get; }
        IDbSet<Dog> Dogs { get; }
        IDbSet<Species> Species { get; }
        IDbSet<Breed> Breeds { get; }
        IDbSet<Category> Categories { get; }
    }
}
