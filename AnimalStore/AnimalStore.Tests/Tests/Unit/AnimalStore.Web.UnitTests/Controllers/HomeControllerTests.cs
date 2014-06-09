using System.Collections.Generic;
using System.Web.Mvc;
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
        private static SearchViewModel _searchViewModel;
        private static ISearchAPIFacade _searchRepository;
        private static HomeController _homeController;
        private static ContactInformation _contactInformation;

        private static readonly List<Breed> breedsList = new List<Breed>()
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
        public void Index_Returns_SearchViewModel()
        {
            // act
            var result = _homeController.Index(null);

            // assert
            Assert.NotNull(result);
            Assert.That(result.Model, Is.EqualTo(_searchViewModel));
        }

        [TestFixture]
        public class When_Dropdown_Is_Populated
        {
            [SetUp]
            public void Init()
            {
                _searchViewModel = MockRepository.GenerateMock<SearchViewModel>();
                _searchRepository = MockRepository.GenerateMock<ISearchAPIFacade>();
                _contactInformation = MockRepository.GenerateMock<ContactInformation>();

                _homeController = new HomeController(_searchViewModel, _searchRepository, _contactInformation);
            }

            [Test]
            public void Index_Doesnt_Call_Repository_GetBreeds()
            {
                // arrange
                var searchViewModel = new SearchViewModel()
                {
                    BreedsSelectList = new SelectList(breedsList)
                };

                // act
                _homeController.Index(searchViewModel);

                // assert
                _searchRepository.AssertWasNotCalled(x => x.GetBreeds());
            }
        }

        [TestFixture]
        public class When_Dropdown_Is_Not_Populated
        {
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
            public void Index_Calls_Repository_GetBreeds()
            {
                // act
                _homeController.Index(null);

                // assert
                _searchRepository.AssertWasCalled(x => x.GetBreeds());
            }
        }
    }
}
