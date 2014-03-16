using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Web.API.Helpers;
using AnimalStore.Web.API.Strategies;
using AnimalStore.Web.API.Wrappers;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace AnimalStore.Services.UnitTests
{
    [TestFixture]
    public class DogSearchManagerTests
    {
        private readonly IDogBreedFilterStrategy _dogBreedFilterStrategy;
        private readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;
        private readonly IDogCategoryService _dogCategoryService;
        private readonly IDogLocationFilterStrategy _dogLocationFilterStrategy;
        private readonly IConfiguration _configuration;

        private DogSearchResultsListBuilder _dogSearchResultsListBuilder;
        private const string _sortColumn = "Something";

        private DogSearchManager _dogSearchManager;

        public DogSearchManagerTests ()
	    {
            _dogBreedFilterStrategy = MockRepository.GenerateMock<IDogBreedFilterStrategy>();
            _dogCategoryFilterStrategy = MockRepository.GenerateMock<IDogCategoryFilterStrategy>();
            _dogCategoryService = MockRepository.GenerateMock<IDogCategoryService>();
            _dogLocationFilterStrategy = MockRepository.GenerateMock<IDogLocationFilterStrategy>();
            _configuration = MockRepository.GenerateMock<IConfiguration>();

            _dogSearchResultsListBuilder = new DogSearchResultsListBuilder();

            _dogBreedFilterStrategy.Stub(x => x.Filter(Arg<int>.Is.Anything)).Return(
                _dogSearchResultsListBuilder.ListOf14Beagels().Build().AsQueryable()
                );

            _dogSearchManager = new DogSearchManager(_dogBreedFilterStrategy
                , _dogCategoryFilterStrategy
                , _dogCategoryService
                , _dogLocationFilterStrategy
                , _configuration);
	    }

        [Test]
        public void GetDogsByBreed_filters_dogs_by_breed()
        {
            // act
            var result = _dogSearchManager.GetDogsByBreed(1);

            // assert
            _dogBreedFilterStrategy.AssertWasCalled(x => x.Filter(1));
            Assert.That(result.Count(), Is.EqualTo(14));
        }


        //[Test]
        //public void ApplyDogLocationFilteringAndSorting_filters_dogs_by_place()
        //{
        //    // act
        //    _dogSearchManager.ApplyDogLocationFilteringAndSorting(_dogs, 1, _sortColumn, 1);

        //    // assert
        //    _dogLocationFilterStrategy.AssertWasCalled(x => x.Filter(_dogs, 1));
        //}

        [Test]
        public void ApplyDogLocationFilteringAndSorting_calls_sorting_strategy_when_not_filtering_on_place()
        {
            var dogs = _dogSearchResultsListBuilder.ListWith30Dogs().Build().AsQueryable();
            _dogCategoryService.Stub(x => x.AddDogsInSameCategoryToDogsCollection(dogs, 1)).Return(dogs);

            // act
            _dogSearchManager.ApplyDogLocationFilteringAndSorting(dogs, 1, _sortColumn, 0);

            // assert
            _dogCategoryFilterStrategy.AssertWasCalled(x => x.Sort(dogs, _sortColumn));
        }

        //[Test]
        //public void ApplyDogLocationFilteringAndSorting_calls_sorting_strategy_when_filtering_on_place()
        //{
        //    // act
        //    _dogSearchHelper.ApplyDogLocationAndSortFiltering(_dogs, 1, _sortColumn, 1);

        //    // assert
        //    _dogLocationFilterStrategy.AssertWasCalled(x => x.Sort(Arg<IEnumerable<Dog>>.Is.Anything));
        //}


    }
}
