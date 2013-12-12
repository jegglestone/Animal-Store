using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using AnimalStore.Model;
using AnimalStore.Web.Controllers;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnimalStore.Web.UnitTests.Controllers
{
    public class SearchControllerTests
    {
        [TestFixture]
        public class SearchControllerDogSearchTests
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

                var SearchController = new SearchController(searchRepository, SessionStateHelper.FakeHttpContext("http://localhost/example"));

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

                var SearchController = new SearchController(searchRepository, SessionStateHelper.FakeHttpContext("http://localhost/example"));

                // act
                var result = (ViewResult) SearchController.Dogs(_searchViewModel);

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

                var SearchController = new SearchController(searchRepository, SessionStateHelper.FakeHttpContext("http://localhost/example"));

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

                var session = SessionStateHelper.FakeHttpContext("http://localhost/example");
                var SearchController = new SearchController(searchRepository, session);

                // act
                var result = (ViewResult) SearchController.Dogs(_searchViewModel);
                var searchViewModelFromSession = (SearchViewModel) session["searchViewModel"];

                // assert
                Assert.That(result.Model, Is.EqualTo(pageableResults));
                Assert.That(searchViewModelFromSession.SelectedBreed, Is.EqualTo(3));
                Assert.That(searchViewModelFromSession.PageNumber, Is.EqualTo(1));
            }
        }

        [TestFixture]
        public class DogSearchSearchViewModelSessionTests
        {
            [Test]
            public void DogSearch_Correctly_Adds_SearchViewModel_To_the_Session ()
            {
                var searchViewModel = MockRepository.GenerateMock<SearchViewModel>();

                const bool isNationalSearch = true;
                const int selectedBreed = 3;
                const int pageNumber = 2;
                const string sortBy = "price";

                searchViewModel.IsNationalSearch = isNationalSearch;
                searchViewModel.SelectedBreed = selectedBreed;
                searchViewModel.PageNumber = pageNumber;
                searchViewModel.SortBy = sortBy;

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
                searchRepository.Stub(x => x.GetDogs(pageNumber, 25, selectedBreed, sortBy)).Return(pageableResults);


                var session = SessionStateHelper.FakeHttpContext("http://localhost/test");
                var SearchController = new SearchController(searchRepository, session);

                // act
                var result = (ViewResult) SearchController.Dogs(searchViewModel);
                var searchViewModelFromSession = (SearchViewModel) session["searchViewModel"];

                // assert
                Assert.That(result.Model, Is.EqualTo(pageableResults));
                Assert.That(searchViewModelFromSession.SelectedBreed, Is.EqualTo(selectedBreed));
                Assert.That(searchViewModelFromSession.PageNumber, Is.EqualTo(pageNumber));
                Assert.That(searchViewModelFromSession.IsNationalSearch, Is.EqualTo(isNationalSearch));
                Assert.That(searchViewModelFromSession.SortBy, Is.EqualTo(sortBy));
            }
        }
    }

    public static class SessionStateHelper
    {
        public static HttpSessionState FakeHttpContext(string url)
        {
            var uri = new Uri(url);
            var httpRequest = new HttpRequest(string.Empty, uri.ToString(),
                                                uri.Query.TrimStart('?'));
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id",
                                                                    new SessionStateItemCollection(),
                                                                    new HttpStaticObjectsCollection(),
                                                                    10, true, HttpCookieMode.AutoDetect,
                                                                    SessionStateMode.InProc, false);

            SessionStateUtility.AddHttpSessionStateToContext(
                httpContext, sessionContainer);

            return httpContext.Session;
        }
    }
}

