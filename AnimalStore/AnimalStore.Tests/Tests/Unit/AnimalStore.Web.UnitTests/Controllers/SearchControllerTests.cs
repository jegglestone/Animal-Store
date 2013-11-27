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
    public class SearchControllerTests
    {
        private SearchViewModel _searchViewModel;

        private readonly List<Breed> breedsList = new List<Breed>()
            {
                new Breed() { Name = "Dalmatian" },
                new Breed() { Name = "Afghan Hound" },
                new Breed() { Name = "Rottweiler" },
                new Breed() { Name = "Whippet" },
                new Breed() { Name = "Blood Hound" },
            };

        [TestFixtureSetUp]
        public void SearchControllerTestsSetup()
        {
            _searchViewModel = MockRepository.GenerateMock<SearchViewModel>();
        }

        [Test]
        public void DogSearch_NationalSearch_Calls_SearchRepository()
        {
            // arrange
            var searchRepository = MockRepository.GenerateMock<ISearchRepository>();
            searchRepository.Stub(x => x.GetDogs(1, 25)).Return(
                new PageableResults<Dog>()
                    {
                        Data = new List<Dog>()
                            {
                                new Dog() {AgeInMonths = 3, AgeInYears = 0, Id = 1, isFemale = false, isSold = false},
                                new Dog() {AgeInMonths = 15, AgeInYears = 1, Id = 3, isFemale = true, isSold = false},
                            }
                    });

            _searchViewModel.IsNationalSearch = true;

            var SearchController = new SearchController(searchRepository);

            // act
            SearchController.DogSearch(_searchViewModel);

            // assert
            searchRepository.AssertWasCalled(x => x.GetDogs(1, 25));
        }

        [Test]
        public void DogSearch_NationalSearch_Return_Redirect_To_DogSearchResults_With_Full_List_Of_Dogs_Found()
        {
            var dogs = new List<Dog>()
                    {
                        new Dog() {AgeInMonths = 3, AgeInYears = 0, Id = 1, isFemale = false, isSold = false},
                        new Dog() {AgeInMonths = 15, AgeInYears = 1, Id = 3, isFemale = true, isSold = false},
                    };
            var pageableResults = new PageableResults<Dog>()
                {
                    Data = dogs
                };

            var searchRepository = MockRepository.GenerateMock<ISearchRepository>();
            searchRepository.Stub(x => x.GetDogs(1, 25)).Return(pageableResults);

            _searchViewModel.IsNationalSearch = true;

            var SearchController = new SearchController(searchRepository);

            // act
            var result = (RedirectToRouteResult)SearchController.DogSearch(_searchViewModel);

            // assert
            Assert.That(result.RouteValues["controller"], Is.EqualTo("SearchResults"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("DogSearchResults"));
            Assert.That(result.RouteValues["searchResults"], Is.EqualTo(pageableResults));
        }
    }
}
