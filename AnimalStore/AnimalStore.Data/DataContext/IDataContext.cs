using System.Data.Entity;
using AnimalStore.Model;

namespace AnimalStore.Data.DataContext
{
    public interface IDataContext
    {
        IDbSet<Animal> Animals { get; set; }
        IDbSet<Species> Species { get; set; }
        IDbSet<Breed> Breeds { get; set; }
    }
}
