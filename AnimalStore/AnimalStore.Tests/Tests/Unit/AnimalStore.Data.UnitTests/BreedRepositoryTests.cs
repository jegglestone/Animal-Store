using AnimalStore.Data.DataContext;
using AnimalStore.Data.Repositories;
using AnimalStore.Data.UnitTests.Fakes;
using AnimalStore.Model;
using NUnit.Framework;

namespace AnimalStore.Data.UnitTests
{
    [TestFixture]
    public class BreedRepositoryTests
    {
        private IAnimalsDataContext _fakeDbContext;

        [SetUp]
        public void SetUp()
        {
            _fakeDbContext = new FakeAnimalsDbContext();
        }

        [Test]
        public void GetById_ExecutesTheQuery()
        {
            var dogSpecies = new Species { Id = 1, Name = "Dog" };
            var YorkieTerrier = new Breed { Id = 1, Name = "Yorkshire Terrier", Species = dogSpecies };

            using (var uow = new UnitsOfWork.UnitOfWork<FakeAnimalsDbContext>(_fakeDbContext))
            {
                using (var repo = new BreedsRepository(uow))
                {
                    //act
                    repo.Add(YorkieTerrier);

                    //assert
                    Assert.That(repo.GetById(1), Is.EqualTo(YorkieTerrier));
                }
            }
        }

        [Test]
        public void Add_AddsObjectT()
        {
            var dogSpecies = new Species { Id = 1, Name = "Dog" };
            var dalmatian = new Breed { Id = 1, Name = "Dalmatian", Species = dogSpecies };

            using (var uow = new UnitsOfWork.UnitOfWork<FakeAnimalsDbContext>(_fakeDbContext))
            {
                using (var repo = new BreedsRepository(uow))
                {
                    //act
                    repo.Add(dalmatian);

                    //assert
                    Assert.That(repo.GetById(1), Is.EqualTo(dalmatian));
                }
            }
        }

        [Test]
        public void Delete_DeletesObjectT()
        {
            var dogSpecies = new Species { Id = 1, Name = "Dog" };
            var dalmatian = new Breed { Id = 1, Name = "Dalmatian", Species = dogSpecies };

            using (var uow = new UnitsOfWork.UnitOfWork<FakeAnimalsDbContext>(_fakeDbContext))
            {
                using (var repo = new BreedsRepository(uow))
                {
                    repo.Add(dalmatian);

                    //act
                    repo.Delete(1);

                    //assert
                    Assert.That(repo.Context.Entry(dalmatian).State.ToString() == "Deleted");
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
