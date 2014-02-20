using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Web.API.Helpers;
using AnimalStore.Web.API.Strategies;
using AnimalStore.Web.API.Wrappers;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Services.UnitTests
{
    [TestFixture]
    public class DogSearchHelperTests
    {
        DogSearchHelper _dogSearchHelper;
        IDogBreedFilterStrategy _dogBreedFilterStrategy;
        IDogCategoryFilterStrategy _dogCategoryFilterStrategy;
        IDoglocationFilterStrategy _dogLocationFilterStrategy;
        IRepository<Breed> _breedsRepository;
        IConfiguration _configuration;

        public DogSearchHelperTests ()
	    {
            _dogBreedFilterStrategy = MockRepository.GenerateMock<IDogBreedFilterStrategy>();
            _dogCategoryFilterStrategy = MockRepository.GenerateMock<IDogCategoryFilterStrategy>();
            _breedsRepository = MockRepository.GenerateMock<IRepository<Breed>>();
            _configuration = MockRepository.GenerateMock<IConfiguration>();

            _configuration.Stub(x => x.GetSearchResultsMinimumMatchingNumber()).Return(5);
            _dogBreedFilterStrategy.Stub(x => x.Filter(Arg<int>.Is.Anything))
                .Return(
                    new List<Dog>().AsQueryable<Dog>()
                );

            _breedsRepository.Stub(x => x.GetById(Arg<int>.Is.Anything))
                .Return (
                    new Breed { Category = new Category { Id = 1 } }
                );

            _dogCategoryFilterStrategy.Stub(x => x.Filter(Arg<int>.Is.Anything))
                .Return(
                    new List<Dog>().AsQueryable<Dog>()
                );

            _dogSearchHelper = new DogSearchHelper(_dogBreedFilterStrategy, _dogCategoryFilterStrategy, _dogLocationFilterStrategy, _breedsRepository, _configuration);
	    }

        [Test]        
        public void GetSortedDogsList_filters_dogs_by_breed()
        {
            // act
            _dogSearchHelper.GetSortedDogsList(1, null);

            // assert
            _dogBreedFilterStrategy.AssertWasCalled(x => x.Filter(1));
        }

        [Test]
        public void GetSortedDogsList_calls_sorting_strategy()
        {
            const string sortColumn = "MyColumn";

            // act
            _dogSearchHelper.GetSortedDogsList(1, sortColumn);

            // assert
            _dogCategoryFilterStrategy.Sort(Arg<IQueryable<Dog>>.Is.Anything, sortColumn);
        }

        [Test]
        public void GetSortedDogsList_appends_dogs_in_same_category_where_there_are_too_few_breed_results()
        {

        }

        [Test]
        public void GetSortedDogsList_when_appending_dogs_in_same_category_then_there_are_no_duplicates_returned()
        {

        }

        [Test]
        public void GetSortedDogsList_filters_dogs_by_place()
        {

        }

        [Test]
        public void GetSortedDogsList_calls_sorting_strategy_when_filtering_on_place()
        {

        }

        [Test]
        public void GetSortedDogsList_appends_dogs_in_same_region_where_there_are_too_few_Place_results()
        {

        }

        [Test]
        public void GetSortedDogsList_when_appending_dogs_in_same_region_then_there_are_no_duplicates_returned()
        {

        }
    }
}
