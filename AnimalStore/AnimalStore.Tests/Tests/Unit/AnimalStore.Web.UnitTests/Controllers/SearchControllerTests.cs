using System.Collections.Generic;
using AnimalStore.Model;
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
    }
}
