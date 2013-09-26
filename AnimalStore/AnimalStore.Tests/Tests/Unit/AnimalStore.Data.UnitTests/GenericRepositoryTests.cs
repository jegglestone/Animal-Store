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
        private IAnimalsDataContext _fakeDbContext;

        [SetUp]
        public void SetUp()
        {
            _fakeDbContext = new FakeAnimalsDbContext();

            //var dogSpecies = new Species { Name = "Dog" };
            //var dalmatian = new Breed { Name = "Dalmatian", Species = dogSpecies };
            //var goldenRetriever = new Breed { Name = "Golden Retriever", Species = dogSpecies };

            //_dbContext.Species.Add(dogSpecies);
            //_dbContext.Breeds.Add(dalmatian);
            //_dbContext.Breeds.Add(goldenRetriever);

            //_dbContext.Animals.Add(new Animal { Id=1, Age = 4, Desc = "A well behaved dalmatian.", Name = "Jessie", isLitter = false, isSold = false, Breed = dalmatian });
            //_dbContext.Animals.Add(new Animal { Id=2, Age = 1, Desc = "A young Golden Retriever. Well behaved and trained.", Name = "Goldie", isLitter = false, isSold = false, Breed = goldenRetriever });

        }

        [Test]
        public void GetById_ExecutesTheQuery()
        {
            var dogSpecies = new Species { Id=1, Name = "Dog" };
            var dalmatian = new Breed { Id=1, Name = "Dalmatian", Species = dogSpecies };
            var testDog = new Animal { Id = 1, AgeInYears = 4, Desc = "A well behaved dalmatian.", Name = "Jessie", isLitter = false, isSold = false, Breed = dalmatian };

            using (var uow = new UnitsOfWork.UnitOfWork<FakeAnimalsDbContext>(_fakeDbContext))
            {
                using (var repo = new AnimalsRepository(uow))
                {
                   //act
                    repo.Add(testDog);

                    //assert
                    Assert.That(repo.GetById(1), Is.EqualTo(testDog));
                }
            }
        }

        [Test]
        public void Add_AddsObjectT()
        {
            var dogSpecies = new Species { Id = 1, Name = "Dog" };
            var dalmatian = new Breed { Id = 1, Name = "Dalmatian", Species = dogSpecies };
            var testDog = new Animal { Id = 1, AgeInYears = 4, Desc = "A well behaved dalmatian.", Name = "Jessie", isLitter = false, isSold = false, Breed = dalmatian };

            using (var uow = new UnitsOfWork.UnitOfWork<FakeAnimalsDbContext>(_fakeDbContext))
            {
                using (var repo = new AnimalsRepository(uow))
                {
                    //act
                    repo.Add(testDog);

                    //assert
                    Assert.That(repo.GetById(1), Is.EqualTo(testDog));
                }
            }
        }

        [Test]
        public void Delete_DeletesObjectT()
        {
            var dogSpecies = new Species { Id = 1, Name = "Dog" };
            var dalmatian = new Breed { Id = 1, Name = "Dalmatian", Species = dogSpecies };
            var testDog = new Animal() { Id = 1, AgeInYears = 4, Desc = "A well behaved dalmatian.", Name = "Jessie", isLitter = false, isSold = false, Breed = dalmatian };

            using (var uow = new UnitsOfWork.UnitOfWork<FakeAnimalsDbContext>(_fakeDbContext))
            {
                using (var repo = new AnimalsRepository(uow))
                {          
                    repo.Add(testDog);

                    //act
                    repo.Delete(1);

                    //assert
                    Assert.That(repo.Context.Entry(testDog).State.ToString() == "Deleted");
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            _fakeDbContext.Dispose();
        }
    }
}
