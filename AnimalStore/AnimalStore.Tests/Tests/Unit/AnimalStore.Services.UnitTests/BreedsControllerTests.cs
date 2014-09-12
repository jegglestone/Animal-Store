using System.Collections.Generic;
using System.Linq;
using AnimalStore.Data.Repositories.Animals;
using AnimalStore.Model;
using AnimalStore.Web.API.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnimalStore.Services.UnitTests
{
    [TestFixture]
    public class BreedsControllerTests
    {
        private readonly IRepository<Breed> _repository;

        public BreedsControllerTests()
        {
            _repository = MockRepository.GenerateMock<IRepository<Breed>>();

            StubRepositoryGetAll();
        }

        private void StubRepositoryGetAll()
        {
            var breedsListWith30Items = new List<Breed>()
            {
                new Breed() { Name = "Dalmatian" },
                new Breed() { Name = "Afghan Hound" },
                new Breed() { Name = "Rottweiler" },
                new Breed() { Name = "Whippet" },
                new Breed() { Name = "Blood Hound" },
            };
            _repository.Stub(x => x.GetAll()).Return(breedsListWith30Items.AsQueryable());
        }

        [Test]
        public void Get_CallsRepositoryGetAllMethod()
        {
            // arrange
            var breedsController = new BreedsController(_repository);

            // act
            breedsController.Get();

            // assert
            _repository.AssertWasCalled(X => X.GetAll());
        }

         [Test]
         public void Get_ReturnsListOfBreedOrderedAlphabeticallyAscending()
         {
             // arrange
             var breedsController = new BreedsController(_repository);
    
             // act
             var result = breedsController.Get().ToList();

             // assert
             Assert.That(result, Is.TypeOf<List<Breed>>());
             Assert.That(result.First().Name, Is.EqualTo("Afghan Hound"));
             Assert.That(result.Last().Name, Is.EqualTo("Whippet"));
         }

        [TearDown]
        public void TearDown()
        {
            _repository.Dispose();
        }
    }
}
