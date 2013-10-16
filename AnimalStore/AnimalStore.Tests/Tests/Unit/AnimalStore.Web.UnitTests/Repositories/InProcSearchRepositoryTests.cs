using System.Collections.Generic;
using System.Linq;
using AnimalStore.Model;
using AnimalStore.Web.API.Controllers;
using AnimalStore.Web.Repository;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnimalStore.Web.UnitTests.Repositories
{
    [TestFixture]
    public class InProcSearchRepositoryTests
    {
        private readonly IController<Breed> _mockBreedsController;

        private readonly List<Breed> breedsList = new List<Breed>()
            {
                new Breed() { Name = "Dalmatian" },
                new Breed() { Name = "Afghan Hound" },
                new Breed() { Name = "Rottweiler" },
                new Breed() { Name = "Whippet" },
                new Breed() { Name = "Blood Hound" },
            };

        public InProcSearchRepositoryTests()
        {
            _mockBreedsController = MockRepository.GenerateMock<IController<Breed>>();
 
            _mockBreedsController.Stub(x => x.Get()).Return(breedsList.AsQueryable());
        }
            
        [Test]
        public void GetBreeds_Calls_API_Get()
        {
            // arrange
            var inProcSearchRepository = new InProcSearchRepository(_mockBreedsController);

            // act
            inProcSearchRepository.GetBreeds();

            // assert
            _mockBreedsController.AssertWasCalled(controller => controller.Get());
        }

        [Test]
        public void GetBreeds_returns_ListOfBreeds()
        {
            // arrange
            var inProcSearchRepository = new InProcSearchRepository(_mockBreedsController);

            // act
            var results = inProcSearchRepository.GetBreeds();

            // assert
            Assert.That(results, Is.EqualTo(breedsList));
        }
    }
}
