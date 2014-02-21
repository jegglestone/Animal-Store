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

        const string sortColumn = "MyColumn";

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
            // arrange
            var dog = new Dog() { Id = 1, BreedId = 1 };

            var dogs = new List<Dog>() { dog }.AsQueryable<Dog>();

            // act
            _dogSearchHelper.AddDogsInSameCategoryToDogsCollection(dogs, 1);

            // assert
            _breedsRepository.AssertWasCalled(x => x.GetById(1));
        }

        [Test]
        public void AddDogsInSameCategoryToDogsCollection_when_appending_dogs_in_same_category_then_there_are_no_duplicates_returned()
        {
            // arrange
            var dog = new Dog() { Id = 1, BreedId = 1 };

            var dogs = new List<Dog>() { dog }.AsQueryable<Dog>();
            var dogsInSameCategory = new List<Dog>() { dog }.AsQueryable<Dog>();

            var dogCategoryFilterStrategy = MockRepository.GenerateMock<IDogCategoryFilterStrategy>();
            var dogBreedFilterStrategy = MockRepository.GenerateMock<IDogBreedFilterStrategy>();

            dogCategoryFilterStrategy.Stub(x => x.Filter(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Return(dogs);

            // act
            var dogSearchHelper = new DogSearchHelper(dogBreedFilterStrategy, dogCategoryFilterStrategy, _dogLocationFilterStrategy, _breedsRepository, _configuration);
            var result = dogSearchHelper.AddDogsInSameCategoryToDogsCollection(dogs, 1);

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ApplyDogLocationAndSortFiltering_filters_dogs_by_place()
        {

        }

        [Test]
        public void ApplyDogLocationAndSortFiltering_calls_sorting_strategy_when_not_filtering_on_place()
        {
            var dog = new Dog() { Id = 1, BreedId = 1 };
            var dogs = new List<Dog>() { dog }.AsQueryable<Dog>();

            // act
            _dogSearchHelper.ApplyDogLocationAndSortFiltering(dogs, 1, sortColumn, 0);

            // assert
            _dogCategoryFilterStrategy.AssertWasCalled(x => x.Sort(dogs, sortColumn));
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
