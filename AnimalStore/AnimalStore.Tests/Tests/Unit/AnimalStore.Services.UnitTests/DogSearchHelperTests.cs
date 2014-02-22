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
    public class DogSearchHelperTests
    {
        readonly DogSearchHelper _dogSearchHelper;
        readonly IDogBreedFilterStrategy _dogBreedFilterStrategy;
        readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;
        readonly IDogLocationFilterStrategy _dogLocationFilterStrategy;
        readonly IRepository<Breed> _breedsRepository;
        readonly IConfiguration _configuration;
        readonly IQueryable<Dog> _dogs;

        const string _sortColumn = "MyColumn";

        public DogSearchHelperTests ()
	    {
            _dogBreedFilterStrategy = MockRepository.GenerateMock<IDogBreedFilterStrategy>();
            _dogCategoryFilterStrategy = MockRepository.GenerateMock<IDogCategoryFilterStrategy>();
            _dogLocationFilterStrategy = MockRepository.GenerateMock<IDogLocationFilterStrategy>();
            _breedsRepository = MockRepository.GenerateMock<IRepository<Breed>>();
            _configuration = MockRepository.GenerateMock<IConfiguration>();

            _dogs = new List<Dog>() { new Dog() { Id = 1, BreedId = 1 } }.AsQueryable<Dog>();

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

            _dogLocationFilterStrategy.Stub(x => x.Filter(Arg<IQueryable<Dog>>.Is.Anything, Arg<int>.Is.Anything))
                .Return(
                    new List<Dog>().AsQueryable<Dog>()
                );

            _dogSearchHelper = new DogSearchHelper(_dogBreedFilterStrategy, _dogCategoryFilterStrategy, _dogLocationFilterStrategy, _breedsRepository, _configuration);
	    }

        [Test]        
        public void GetDogsList_filters_dogs_by_breed()
        {
            // act
            _dogSearchHelper.GetDogsList(1, null);

            // assert
            _dogBreedFilterStrategy.AssertWasCalled(x => x.Filter(1));
        }

        [Test]
        public void AddDogsInSameCategoryToDogsCollection_appends_dogs_in_same_category_where_there_are_too_few_breed_results()
        {
            // act
            _dogSearchHelper.AddDogsInSameCategoryToDogsCollection(_dogs, 1);

            // assert
            _breedsRepository.AssertWasCalled(x => x.GetById(1));
        }

        [Test]
        public void AddDogsInSameCategoryToDogsCollection_when_appending_dogs_in_same_category_then_there_are_no_duplicates_returned()
        {
            // arrange
            var dogCategoryFilterStrategy = MockRepository.GenerateMock<IDogCategoryFilterStrategy>();
            var dogBreedFilterStrategy = MockRepository.GenerateMock<IDogBreedFilterStrategy>();

            dogCategoryFilterStrategy.Stub(x => x.Filter(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Return(_dogs);

            // act
            var dogSearchHelper = new DogSearchHelper(dogBreedFilterStrategy, dogCategoryFilterStrategy, _dogLocationFilterStrategy, _breedsRepository, _configuration);
            var result = dogSearchHelper.AddDogsInSameCategoryToDogsCollection(_dogs, 1);

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ApplyDogLocationAndSortFiltering_filters_dogs_by_place()
        {
            // act
            _dogSearchHelper.ApplyDogLocationAndSortFiltering(_dogs, 1, _sortColumn, 1);

            // assert
            _dogLocationFilterStrategy.AssertWasCalled(x => x.Filter(_dogs, 1));
        }

        [Test]
        public void ApplyDogLocationAndSortFiltering_calls_sorting_strategy_when_not_filtering_on_place()
        {
            // act
            _dogSearchHelper.ApplyDogLocationAndSortFiltering(_dogs, 1, _sortColumn, 0);

            // assert
            _dogCategoryFilterStrategy.AssertWasCalled(x => x.Sort(_dogs, _sortColumn));
        }

        [Test]
        public void ApplyDogLocationAndSortFiltering_calls_sorting_strategy_when_filtering_on_place()
        {
            // act
            _dogSearchHelper.ApplyDogLocationAndSortFiltering(_dogs, 1, _sortColumn, 1);

            // assert
            _dogLocationFilterStrategy.AssertWasCalled(x => x.Sort(Arg<IEnumerable<Dog>>.Is.Anything));
        }

        [Test]
        public void ApplyDogLocationAndSortFiltering_appends_dogs_in_same_region_where_there_are_too_few_Place_results()
        {

        }

        [Test]
        public void ApplyDogLocationAndSortFiltering_when_appending_dogs_in_same_region_then_there_are_no_duplicates_returned()
        {

        }
    }
}
