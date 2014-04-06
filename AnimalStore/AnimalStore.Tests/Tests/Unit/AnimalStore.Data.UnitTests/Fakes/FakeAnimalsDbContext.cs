using System.Data.Entity;
using AnimalStore.Data.DataContext;
using AnimalStore.Model;
using Rhino.Mocks;

namespace AnimalStore.Data.UnitTests.Fakes
{
    internal class FakeAnimalsDbContext : DbContext, IAnimalsDataContext
    {
        public IDbSet<Animal> Animals { get; set; }
        public IDbSet<Dog> Dogs { get; set; }
        public IDbSet<Species> Species { get; set; }
        public IDbSet<Breed> Breeds { get; set; }
        public IDbSet<Category> Categories { get; set; }

        public FakeAnimalsDbContext()
        {
            Animals = MockRepository.GenerateMock<IDbSet<Animal>>();
            Dogs = MockRepository.GenerateMock<IDbSet<Dog>>();
            Breeds = MockRepository.GenerateMock<IDbSet<Breed>>();
            Species = MockRepository.GenerateMock<IDbSet<Species>>();

            Database.SetInitializer<FakeAnimalsDbContext>(null);
        }

        public new int SaveChanges()
        {
            return 1;
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
