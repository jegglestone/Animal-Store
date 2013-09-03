using System.Data.Entity;

namespace AnimalStore.Data.UnitTests.Fakes
{
    internal class FakeDbContext
    {
        public IDbSet<Animal> Animals { get; set; }
        public IDbSet<Species> Species { get; set; }
        public IDbSet<Breed> Breeds { get; set; }


    }
}
