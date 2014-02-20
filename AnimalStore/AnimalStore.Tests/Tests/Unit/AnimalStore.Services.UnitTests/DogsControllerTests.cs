using AnimalStore.Common.Constants;
using AnimalStore.Web.API.Controllers;
using AnimalStore.Data.Repositories;
using AnimalStore.Web.API.Helpers;
using AnimalStore.Web.API.Wrappers;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Linq;
using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;
using AnimalStore.Services.UnitTests.SUT_Builder_Factories;

namespace AnimalStore.Services.UnitTests
{
    [TestFixture]
    public class DogsControllerTests
    {
        private IRepository<Dog> _dogsRepository;
        private IRepository<Breed> _breedsRepository;
        private IDogSearchHelper _dogSearchhelper;
        private IUnitOfWork _unitofWork;
        private DogsController _dogsController;
        private IConfiguration _configuration;

        private Category _category;
        private Breed _bloodhound;
        private Breed _beagle;

        [TestFixtureSetUp]
        public void DogsControllerTestsSetup()
        {
            _dogsRepository = MockRepository.GenerateMock<IRepository<Dog>>();
            _breedsRepository = MockRepository.GenerateMock<IRepository<Breed>>();
            _unitofWork = MockRepository.GenerateMock<IUnitOfWork>();
            _dogSearchhelper = MockRepository.GenerateMock<IDogSearchHelper>();
            _configuration = MockRepository.GenerateMock<IConfiguration>();

            _breedsRepository.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(
                new Breed { Name="Beagel"});

            _configuration.Stub(x => x.GetNationwideSearchResultsDescriptionMessageForAllBreeds()).Return("Search results {0} to {1} out of {2} results for all breeds nationwide.");
            _configuration.Stub(x => x.GetNationwideSearchResultsDescriptionMessageForSpecificBreed()).Return("Showing results {0} to {1} out of {2} results for {3} nationwide");

            _dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogSearchhelper, _configuration);

            _category = new Category() { Description = "Dogs for hunting foxes and badgers etc.", Id = 3, Name = "Hunting" };

            _bloodhound = new Breed() { Name = "Bloodhound", Category = _category, Id = 3, Species = null };
            _beagle = new Breed() { Name = "Beagel", Category = _category, Id = 3, Species = null };

            StubDogsRepository();
        }

        private void StubDogsRepository()
        {
            _dogsRepository.Stub(x => x.GetById(4)).Return(new Dog() { Name = "dog", Id = 4 });
            _dogsRepository.Stub(x => x.GetAll()).Return(
                new DogSearchResultsListBuilder().ListWith30Dogs().Build().AsQueryable());
        }

        [Test]
        public void Get_Paged_With_Breed_Returns_MatchingDogs_And_Dogs_In_The_Same_Category_Beneath()
        {
            var bloodhoundHuntingDog = new Dog() { Name = "Tip", Breed = _bloodhound };
            var beagleHuntingDog = new Dog() { Name = "Shep", Breed = _beagle };

            var matchedDogs = new DogSearchResultsListBuilder()
                .WithAnotherDog(beagleHuntingDog)
                .WithAnotherDog(bloodhoundHuntingDog)
                .Build();

            var dogSearchhelper = MockRepository.GenerateMock<IDogSearchHelper>();
            dogSearchhelper.Stub(x => x.GetSortedDogsList(3, SearchSortOptions.PRICE_HIGHEST)).Return(matchedDogs);

            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, dogSearchhelper, _configuration);

            //act
            var result = dogsController.GetPaged(3, 1, 20, SearchSortOptions.PRICE_HIGHEST);

            Assert.That(result.Data.First(), Is.EqualTo(beagleHuntingDog));
            Assert.That(result.Data.Contains(bloodhoundHuntingDog));
        }

        [Test]
        public void Get_Paged_With_Breed_Returns_Correct_Search_Description()
        {
            // arrange
            const int breedId = 3;
            const int page = 2;
            const int pageSize = 5;

            _dogSearchhelper.Stub(x => x.GetSortedDogsList(breedId, SearchSortOptions.PRICE_HIGHEST)).Return(new DogSearchResultsListBuilder().ListOf14Beagels().Build());

            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogSearchhelper, _configuration);

            //act
            var result = dogsController.GetPaged(breedId, page, pageSize, SearchSortOptions.PRICE_HIGHEST);

            Assert.That(result.SearchDescription, Is.EqualTo("Showing results 6 to 10 out of 14 results for Beagel nationwide"));
        }

        [Test]
        public void Get_CallRepositoryGetAllMethod()
        {
            // act
            _dogsController.GetPaged();

            // assert
            _dogsRepository.AssertWasCalled(X => X.GetAll());
        }

        [Test]
        public void Get_ReturnsUpTo25Items_WithNoPageLimitSpecified()
        {
            // act
            var result = _dogsController.GetPaged();

            // assert
            Assert.That(result.Data.Count(), Is.EqualTo(25));
        }

        [Test]
        public void Get_ReturnsSpecifiedNumberOfResultSetWhenPaging()
        {
            // act
            var result = _dogsController.GetPaged(1, 10);

            // assert
            Assert.That(result.Data.Count(), Is.EqualTo(10));
        }

        [TestCase(1, 10, "Flossie", Description = "we expect the first page to have this dog")]
        [TestCase(2, 10, "Rex", Description = "we expect the second page to have this dog")]
        [TestCase(3, 10, "Tip", Description = "we expect the third page to have this dog")]
        public void get_ReturnsTheSpecifiedPage(int pageNumber, int pageSize, string expectedDogName)
        {
            // act
            var result = _dogsController.GetPaged(pageNumber, pageSize);

            // assert
            Assert.That(result.Data.ToList().First(dog => dog.Name == expectedDogName), Is.Not.Null);
        }

        [Test]
        public void GetPaged_ReturnsTheCorrectTotalCount()
        {
            // act
            var result = _dogsController.GetPaged(1, 10);

            // assert
            Assert.That(result.TotalCount, Is.EqualTo(30));
        }

        [Test]
        public void GetPaged_ReturnsTheCorrectPageCount()
        {
            // act
            var result = _dogsController.GetPaged(1, 9);

            // assert
            Assert.That(result.TotalPages, Is.EqualTo(4));
        }

        [Test]
        public void GetPaged_ReturnsTheCurrentPage()
        {
            // arrange
            const int page = 2;

            // act
            var result = _dogsController.GetPaged(page, 4);

            // assert
            Assert.That(result.CurrentPageNumber, Is.EqualTo(page));
        }

        [Test]
        public void GetPaged_ReturnsTheCorrectNextPageUrl()
        {
            // act
            var result = _dogsController.GetPaged(1, 10);

            // assert
            Assert.That(result.NextPage.Contains("?page=2"));
        }

        [Test]
        public void GetPaged_ReturnsTheCorrectPrevPageUrl()
        {
            // act
            var result = _dogsController.GetPaged(2, 10);

            // assert
            Assert.That(result.PrevPage.Contains("?page=1"));
        }

        [Test]
        public void GetPaged_ReturnsItemsOrderedByDateCreatedDescending()
        {
            // act
            var result = _dogsController.GetPaged(0, 20);

            // assert
            Assert.That(result.Data.ToList()[0].CreatedOn, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void Get_ById_ReturnsSingleItemWithMatchingIdAndCallsRepositoryGetById()
        {
            // act
            var result = _dogsController.Get(4);

            // assert
            _dogsRepository.AssertWasCalled(x => x.GetById(4));
            Assert.That(result.Id, Is.EqualTo(4));
        }

        [TearDown]
        public void TearDown()
        {
            _dogsRepository.Dispose();
            _unitofWork.Dispose();
        }
    }
}