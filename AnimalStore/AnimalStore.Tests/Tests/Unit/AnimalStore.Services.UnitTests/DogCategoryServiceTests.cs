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
    public class DogCategoryServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<Breed> _breedsRepository;
        private readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;

        private const int _breedIdDalmatian = 1;
        private const int _breedIdBeagel = 2;
        private const int _categoryId = 3;
        private const int _searchResultsMinimumMatchingNumber = 5;

        public DogCategoryServiceTests()
        {
            _configuration = MockRepository.GenerateMock<IConfiguration>();
            _breedsRepository = MockRepository.GenerateMock<IRepository<Breed>>();
            _dogCategoryFilterStrategy = MockRepository.GenerateMock<IDogCategoryFilterStrategy>();
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            var dogSearchResultsListBuilder = new DogSearchResultsListBuilder();

            _configuration.Stub(x => x.GetSearchResultsMinimumMatchingNumber()).Return(_searchResultsMinimumMatchingNumber);

            _breedsRepository.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(
                new Breed { Id = _breedIdDalmatian, Category = new Category { Id = _categoryId } });

            _dogCategoryFilterStrategy.Stub(x => x.Filter(_categoryId, _breedIdDalmatian)).Return(
                dogSearchResultsListBuilder.ListOf3BeagelsByCategory(_categoryId, _breedIdBeagel).Build().AsQueryable());
        }

        [Test]
        public void AddDogsInSameCategoryToDogsCollection_appends_dogs_in_same_category_when_there_are_too_few_results()
        {
            // arrange
            var dogSearchResultsListBuilder = new DogSearchResultsListBuilder();
            var dogsMatchingByBreed = dogSearchResultsListBuilder.ListOf3DalmatiansByCategory(_categoryId, _breedIdDalmatian).Build().AsQueryable();
            var dogCategoryService = new DogCategoryService(_configuration, _breedsRepository, _dogCategoryFilterStrategy);

            // act
            var results = dogCategoryService.AddDogsInSameCategoryToDogsCollection(dogsMatchingByBreed, _breedIdDalmatian);

            // assert
            Assert.That(results.Count(), Is.EqualTo(6));
        }

        [Test]
        public void AddDogsInSameCategoryToDogsCollection_when_appending_dogs_in_same_category_then_there_are_no_duplicates_returned()
        {
            // arrange
            var dogSearchResultsListBuilder = new DogSearchResultsListBuilder();
            var dogsMatchingByBreed = dogSearchResultsListBuilder.ListOfThreeDuplicateDogs(_categoryId, _breedIdDalmatian).Build().AsQueryable();
            var dogCategoryService = new DogCategoryService(_configuration, _breedsRepository, _dogCategoryFilterStrategy);

            // act
            var results = dogCategoryService.AddDogsInSameCategoryToDogsCollection(dogsMatchingByBreed, _breedIdDalmatian);

            // assert
            Assert.That(results.Count(), Is.EqualTo(4));
        }

        [Test]
        public void AddDogsInSameCategoryToDogsCollection_when_no_matching_dogs_then_return_dogs_in_category()
        {
            // arrange
            var dogSearchResultsListBuilder = new DogSearchResultsListBuilder();
            var dogsMatchingByBreedEmptyList = new List<Dog>().AsQueryable();
            var dogCategoryService = new DogCategoryService(_configuration, _breedsRepository, _dogCategoryFilterStrategy);

            // act
            var results = dogCategoryService.AddDogsInSameCategoryToDogsCollection(dogsMatchingByBreedEmptyList, _breedIdDalmatian);

            // assert
            Assert.That(results.Count(), Is.EqualTo(3));
        }

        [Test]
        public void AddDogsInSameCategoryToDogsCollection_place_matching_dogs_above_dogs_in_same_category()
        {
            // arrange
            var dogSearchResultsListBuilder = new DogSearchResultsListBuilder();
            var dogsMatchingByBreed = dogSearchResultsListBuilder.ListOf3DalmatiansByCategory(_categoryId, _breedIdDalmatian).Build().AsQueryable();
            var dogCategoryService = new DogCategoryService(_configuration, _breedsRepository, _dogCategoryFilterStrategy);

            // act
            var results = dogCategoryService.AddDogsInSameCategoryToDogsCollection(dogsMatchingByBreed, _breedIdDalmatian);

            // assert
            Assert.That(results.First().Breed.Id == _breedIdDalmatian);
            Assert.That(results.Last().Breed.Id == _breedIdBeagel);
        }
    }
}
