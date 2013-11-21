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

        //[Test]
        //public void Index_Redirects_To_HomeController_Index()
        //{
        //    // arrange
        //    var searchController = new SearchController(_searchViewModel, _searchRepository);

        //    // act
        //    var result = searchController.Index();

        //    // assert
        //    Assert.That(result.);
        //}
    }
}
