using System.Data.Entity;
using AnimalStore.Data.DataContext;
using AnimalStore.Model;

namespace AnimalStore.Data.UnitTests.Fakes
{
    internal class FakeDbContext : IDataContext
    {
        public IDbSet<Animal> Animals { get; set; }
        public IDbSet<Species> Species { get; set; }
        public IDbSet<Breed> Breeds { get; set; }

        public FakeDbContext()
        {
            Animals = new FakeAnimalDbSet();
            Species = new FakeSpeciesDbSet();
            Breeds = new FakeBreedDbSet();
        }
    }
}
