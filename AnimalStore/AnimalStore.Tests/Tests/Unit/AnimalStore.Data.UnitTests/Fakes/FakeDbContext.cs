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
           InitialiseSeededTestData();
       }

        private void InitialiseSeededTestData()
        {
            var dogSpecies = new Species { Name = "Dog" };
            var dalmatian = new Breed { Name = "Dalmatian", Species = dogSpecies };
            var goldenRetriever = new Breed { Name = "Golden Retriever", Species = dogSpecies };

            Species.Add(dogSpecies);
            Breeds.Add(dalmatian);
            Breeds.Add(goldenRetriever);

            Animals.Add(new Animal { Age = 4, Desc = "A well behaved dalmatian.", Name = "Jessie", isLitter = false, isSold = false, Breed = dalmatian });
            Animals.Add(new Animal { Age = 1, Desc = "A young Golden Retriever. Well behaved and trained.", Name = "Goldie", isLitter = false, isSold = false, Breed = goldenRetriever });
        }
    }
}
