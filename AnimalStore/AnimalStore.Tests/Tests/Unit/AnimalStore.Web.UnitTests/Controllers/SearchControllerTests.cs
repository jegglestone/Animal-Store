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
using AnimalStore.Web.Wrappers.Interfaces;
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

                var customHttpRequestWrapper = MockRepository.GenerateMock<ICustomHttpRequestWrapper>();

                var SearchController = new SearchController(searchRepository, SessionStateHelper.FakeHttpContext("http://localhost/example"), customHttpRequestWrapper);

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

                var httpRequestWrapper = MockRepository.GenerateMock<ICustomHttpRequestWrapper>();

                var SearchController = new SearchController(searchRepository, SessionStateHelper.FakeHttpContext("http://localhost/example"), httpRequestWrapper);

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

                var httpRequestWrapper = MockRepository.GenerateMock<ICustomHttpRequestWrapper>();

                var SearchController = new SearchController(searchRepository, SessionStateHelper.FakeHttpContext("http://localhost/example"), httpRequestWrapper);

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

                var httpRequestWrapper = MockRepository.GenerateMock<ICustomHttpRequestWrapper>();

                var session = SessionStateHelper.FakeHttpContext("http://localhost/example");
                var SearchController = new SearchController(searchRepository, session, httpRequestWrapper);

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
            const bool _isNationalSearch = true;
            const int _selectedBreed = 3;
            const int _pageNumber = 2;
            private SearchViewModel searchViewModel;
            private List<Dog> _dogs;

            [TestFixtureSetUp]
            public void Setup()
            {
                searchViewModel = MockRepository.GenerateMock<SearchViewModel>();
                searchViewModel.IsNationalSearch = _isNationalSearch;
                searchViewModel.SelectedBreed = _selectedBreed;
                searchViewModel.PageNumber = _pageNumber;

                _dogs = new List<Dog>()
                    {
                        new Dog() {AgeInMonths = 3, AgeInYears = 0, Id = 1, isFemale = false, isSold = false},
                        new Dog() {AgeInMonths = 15, AgeInYears = 1, Id = 3, isFemale = true, isSold = false},
                    };
            }

            [Test]
            public void Dogs_Search_Correctly_Adds_SearchViewModel_To_the_Session ()
            {
                const string sortBy = "price";
                searchViewModel.SortBy = sortBy;

                var pageableResults = new PageableResults<Dog>(){ Data = _dogs };

                var searchRepository = MockRepository.GenerateMock<ISearchAPIFacade>();
                searchRepository.Stub(x => x.GetDogs(_pageNumber, 25, _selectedBreed, sortBy)).Return(pageableResults);

                var session = SessionStateHelper.FakeHttpContext("http://localhost/test?sortBy=price");

                var httpRequestWrapper = MockRepository.GenerateMock<ICustomHttpRequestWrapper>();
                httpRequestWrapper.Stub(x => x.GetQueryStringValueByKey(QuerystringKeys.SortBy)).Return(sortBy);

                var SearchController = new SearchController(searchRepository, session, httpRequestWrapper);

                // act
                var result = (ViewResult) SearchController.Dogs(searchViewModel);
                var searchViewModelFromSession = (SearchViewModel) session["searchViewModel"];

                // assert
                Assert.That(result.Model, Is.EqualTo(pageableResults));
                Assert.That(searchViewModelFromSession.SelectedBreed, Is.EqualTo(_selectedBreed));
                Assert.That(searchViewModelFromSession.PageNumber, Is.EqualTo(_pageNumber));
                Assert.That(searchViewModelFromSession.IsNationalSearch, Is.EqualTo(_isNationalSearch));
                Assert.That(searchViewModelFromSession.SortBy, Is.EqualTo(sortBy));
            }

            [Test]
            public void Dogs_Sorted_Correctly_Retrieves_SearchViewModel_From_Session_And_Adds_SortBy_And_Resets_PageNumber_To_One()
            {
                const string sortBy = "Date(CreatedOn)";

                var pageableResults = new PageableResults<Dog>() { Data = _dogs };

                var searchRepository = MockRepository.GenerateMock<ISearchAPIFacade>();
                searchRepository.Stub(x => x.GetDogs(_pageNumber, 25, _selectedBreed, sortBy)).Return(pageableResults);

                var session = SessionStateHelper.FakeHttpContext("http://localhost/test?sortBy=" + sortBy);
                session["SearchViewModel"] = searchViewModel;

                var httpRequestWrapper = MockRepository.GenerateMock<ICustomHttpRequestWrapper>();
                httpRequestWrapper.Stub(x => x.GetQueryStringValueByKey(QuerystringKeys.SortBy)).Return(sortBy);

                var SearchController = new SearchController(searchRepository, session, httpRequestWrapper);

                // act
                SearchController.DogsSorted();

                // assert
                var searchViewModelFromSession = (SearchViewModel)session["searchViewModel"];
                Assert.That(searchViewModelFromSession.SelectedBreed, Is.EqualTo(_selectedBreed));
                Assert.That(searchViewModelFromSession.PageNumber, Is.EqualTo(1));
                Assert.That(searchViewModelFromSession.IsNationalSearch, Is.EqualTo(_isNationalSearch));
                Assert.That(searchViewModelFromSession.SortBy, Is.EqualTo(sortBy));
            }

            [Test]
            public void Dogs_Sorted_Redirects_ToHome_Controller_If_Session_Is_Expired()
            {
                const string sortBy = "Date(CreatedOn)";

                var pageableResults = new PageableResults<Dog>() { Data = _dogs };

                var searchRepository = MockRepository.GenerateMock<ISearchAPIFacade>();
                searchRepository.Stub(x => x.GetDogs(_pageNumber, 25, _selectedBreed, sortBy)).Return(pageableResults);

                var session = SessionStateHelper.FakeHttpContext("http://localhost/test?sortBy=" + sortBy);
                session["searchViewModel"] = null;

                var httpRequestWrapper = MockRepository.GenerateMock<ICustomHttpRequestWrapper>();
                httpRequestWrapper.Stub(x => x.GetQueryStringValueByKey(QuerystringKeys.SortBy)).Return(sortBy);

                var SearchController = new SearchController(searchRepository, session, httpRequestWrapper);

                // act
                var result = (RedirectToRouteResult)SearchController.DogsSorted();

                // assert
                Assert.That(result.RouteValues["Controller"].Equals("Home"));
                Assert.That(result.RouteValues["Action"].Equals("Index"));
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

