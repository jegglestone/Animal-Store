using AnimalStore.Data.DataContext;
using AnimalStore.Data.Repositories;
using AnimalStore.Data.UnitTests.Fakes;
using AnimalStore.Model;
using NUnit.Framework;

namespace AnimalStore.Data.UnitTests
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        private IAnimalsDataContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new FakeAnimalsDbContext();

            var dogSpecies = new Species { Name = "Dog" };
            var dalmatian = new Breed { Name = "Dalmatian", Species = dogSpecies };
            var goldenRetriever = new Breed { Name = "Golden Retriever", Species = dogSpecies };

            _dbContext.Species.Add(dogSpecies);
            _dbContext.Breeds.Add(dalmatian);
            _dbContext.Breeds.Add(goldenRetriever);

            _dbContext.Animals.Add(new Animal { Id=1, Age = 4, Desc = "A well behaved dalmatian.", Name = "Jessie", isLitter = false, isSold = false, Breed = dalmatian });
            _dbContext.Animals.Add(new Animal { Id=2, Age = 1, Desc = "A young Golden Retriever. Well behaved and trained.", Name = "Goldie", isLitter = false, isSold = false, Breed = goldenRetriever });

        }

        //[Test]
        //public void GenericRepositoryConstructor_ThrowsCorrectExceptionWhenNoDbContextIsInjected()
        //{
        //    //act
        //    var ex = Assert.Throws<Exception>(() => new GenericRepository<Animal>(null));
        //    Assert.That(ex.Message, Is.EqualTo("context"));
        //    Assert.That(ex.InnerException, Is.EqualTo("An instance of DbContext is required to use this generic repository"));
        //}


        [Test]
        public void GetById_ReturnsObjectOfTypeT()
        {

        }

        [Test]
        public void Add_AddsObjectT()
        {
        }

        [Test]
        public void Delete_DeletesObjectT()
        {
        }
    }
}
