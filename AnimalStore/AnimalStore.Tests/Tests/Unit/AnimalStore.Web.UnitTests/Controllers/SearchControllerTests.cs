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

        [TestFixtureSetUp]
        public void SearchControllerTestsSetup()
        {
            _searchViewModel = MockRepository.GenerateMock<SearchViewModel>();
        }

        [Test]
        public void DogSearch_NationalSearch_Calls_SearchRepository_GetDogs_When_NationalSearch_For_all_breeds()
        {
            // arrange
            var searchRepository = MockRepository.GenerateMock<ISearchAPIFacade>();
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
            _searchViewModel.SelectedBreed = 0;

            var SearchController = new SearchController(searchRepository);

            // act
            SearchController.Dogs(_searchViewModel);

            // assert
            searchRepository.AssertWasCalled(x => x.GetDogs(1, 25));
        }

        [Test]
        public void DogSearch_NationalSearch_Return_View_With_Full_List_Of_Dogs_Found_When_NationalSearch_For_all_breeds()
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

            var searchRepository = MockRepository.GenerateMock<ISearchAPIFacade>();
            searchRepository.Stub(x => x.GetDogs(1, 25)).Return(pageableResults);

            _searchViewModel.IsNationalSearch = true;
            _searchViewModel.SelectedBreed = 0;

            var SearchController = new SearchController(searchRepository);

            // act
            var result = (ViewResult)SearchController.Dogs(_searchViewModel);

            // assert
            Assert.That(result.Model, Is.EqualTo(pageableResults));
        }

        [Test]
        public void DogSearch_NationalSearch_Calls_SearchRepository_GetDogsByBreed_When_NationalSearch_For_Specific_Breed()
        {
            // arrange
            var searchRepository = MockRepository.GenerateMock<ISearchAPIFacade>();
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
            _searchViewModel.SelectedBreed = 4;

            var SearchController = new SearchController(searchRepository);

            // act
            SearchController.Dogs(_searchViewModel);

            // assert
            searchRepository.AssertWasCalled(x => x.GetDogs(1, 25, 4));
        }

        [Test]
        public void DogSearch_NationalSearch_Return_View_With_Full_List_Of_Dogs_Found_When_NationalSearch_For_Specific_Breed()
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

            var searchRepository = MockRepository.GenerateMock<ISearchAPIFacade>();
            searchRepository.Stub(x => x.GetDogs(1, 25, 3)).Return(pageableResults);

            _searchViewModel.IsNationalSearch = true;
            _searchViewModel.SelectedBreed = 3;

            var SearchController = new SearchController(searchRepository);

            // act
            var result = (ViewResult)SearchController.Dogs(_searchViewModel);

            // assert
            Assert.That(result.Model, Is.EqualTo(pageableResults));
        }
    }
}
