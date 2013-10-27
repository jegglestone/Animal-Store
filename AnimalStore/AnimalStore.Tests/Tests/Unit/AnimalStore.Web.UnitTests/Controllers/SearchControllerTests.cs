using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly SearchViewModel _searchViewModel;
        private readonly ISearchRepository _searchRepository;

        private readonly List<Breed> breedsList = new List<Breed>()
            {
                new Breed() { Name = "Dalmatian" },
                new Breed() { Name = "Afghan Hound" },
                new Breed() { Name = "Rottweiler" },
                new Breed() { Name = "Whippet" },
                new Breed() { Name = "Blood Hound" },
            };

        public SearchControllerTests()
        {
            _searchViewModel = MockRepository.GenerateMock<SearchViewModel>();
            _searchRepository = MockRepository.GenerateMock<ISearchRepository>();

            _searchRepository.Stub(x => x.GetBreeds()).Return(breedsList);
        }

        [Test]
        public void Index_CallsRepository()
        {
            // arrange
            var searchController = new SearchController(_searchViewModel, _searchRepository);

            // act
            searchController.Index();

            // assert
            _searchRepository.AssertWasCalled(x => x.GetBreeds());
        }

        [Test]
        public void Index_Returns_SearchViewModel()
        {
            // arrange
            var searchController = new SearchController(_searchViewModel, _searchRepository);

            // act
            var result = searchController.Index() as PartialViewResult;

            // assert
            Assert.NotNull(result);
            Assert.That(result.Model, Is.EqualTo(_searchViewModel));
        }
    }
}
