using System.Collections.Generic;
using AnimalStore.Model;
using AnimalStore.Web.Controllers;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnimalStore.Web.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private SearchViewModel _searchViewModel;
        private ISearchAPIFacade _searchRepository;
        private HomeController _homeController;
        private ContactInformation _contactInformation;

        private readonly List<Breed> breedsList = new List<Breed>()
            {
                new Breed() { Name = "Dalmatian" },
                new Breed() { Name = "Afghan Hound" },
                new Breed() { Name = "Rottweiler" },
                new Breed() { Name = "Whippet" },
                new Breed() { Name = "Blood Hound" },
            };

        [SetUp]
        public void Init()
        {
            _searchViewModel = MockRepository.GenerateMock<SearchViewModel>();
            _searchRepository = MockRepository.GenerateMock<ISearchAPIFacade>();
            _contactInformation = MockRepository.GenerateMock<ContactInformation>();

            _searchRepository.Stub(x => x.GetBreeds()).Return(breedsList);
            _homeController = new HomeController(_searchViewModel, _searchRepository, _contactInformation);            
        }

        [Test]
        public void Index_CallsRepository()
        {
            // act
            _homeController.Index();

            // assert
            _searchRepository.AssertWasCalled(x => x.GetBreeds());
        }

        [Test]
        public void Index_Returns_SearchViewModel()
        {
            // act
            var result = _homeController.Index();

            // assert
            Assert.NotNull(result);
            Assert.That(result.Model, Is.EqualTo(_searchViewModel));
        }
    }
}
