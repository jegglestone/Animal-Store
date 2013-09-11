using System.Data.Entity;
using AnimalStore.Data.DataContext;
using AnimalStore.Model;

namespace AnimalStore.Data.UnitTests.Fakes
{
    internal class FakeAnimalsDbContext : DbContext, IAnimalsDataContext
    {
        public IDbSet<Animal> Animals { get; set; }
        public IDbSet<Species> Species { get; set; }
        public IDbSet<Breed> Breeds { get; set; }

        public FakeAnimalsDbContext()
        {
            Animals = new FakeAnimalDbSet();
            Breeds = new FakeBreedDbSet();
            Species = new FakeSpeciesDbSet();
        }

        public new int SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void SetModified(object entity)
        {
            throw new System.NotImplementedException();
        }

        public void SetAdd(object entity)
        {
            throw new System.NotImplementedException();
        }

        public new void Dispose()
        {
        }
    }
}
