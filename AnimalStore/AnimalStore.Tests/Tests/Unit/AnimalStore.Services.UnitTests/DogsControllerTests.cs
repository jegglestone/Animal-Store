using System.Collections.ObjectModel;
using AnimalStore.Common.Constants;
using AnimalStore.Web.API.Controllers;
using AnimalStore.Data.Repositories;
using AnimalStore.Web.API.Helpers;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;

namespace AnimalStore.Services.UnitTests
{
    [TestFixture]
    public class DogsControllerTests
    {
        private readonly IRepository<Dog> _dogsRepository;
        private readonly IDogSearchHelper _dogSearchhelper;
        private readonly IUnitOfWork _unitofWork;
        private readonly DogsController _dogsController;

        public DogsControllerTests()
        {
            _dogsRepository = MockRepository.GenerateMock<IRepository<Dog>>();
            _unitofWork = MockRepository.GenerateMock<IUnitOfWork>();
            _dogSearchhelper = MockRepository.GenerateMock<IDogSearchHelper>();

            _dogsController = new DogsController(_dogsRepository, _unitofWork, _dogSearchhelper);

            StubDogsRepository();
        }

        private void StubDogsRepository()
        {
            var animalsListWith30Items = new List<Dog>()
            {
                new Dog() { Name = "dog1", CreatedOn = DateTime.Today.AddHours(-1) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-1) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-1) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-1) },
                new Dog() { Name = "Flossie", CreatedOn = DateTime.Today.AddHours(-1) },

                new Dog() { Name = "dog", CreatedOn = DateTime.Today },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-1) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-1) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-2) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-2) },

                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-2) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-2) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-2) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-1) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-2) },

                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-1) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-2) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-2) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-2) },
                new Dog() { Name = "Rex", CreatedOn = DateTime.Today.AddHours(-2) },

                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-3) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-3) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-3) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-3) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-3) },

                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-3) },
                new Dog() { Name = "Tip", CreatedOn = DateTime.Today.AddHours(-3) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-3) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-3) },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-3) },
            };
            _dogsRepository.Stub(x => x.GetAll()).Return(animalsListWith30Items.AsQueryable());
        }

        [Test]
        public void Get_Paged_With_Breed_Returns_MatchingDogs_And_Dogs_In_The_Same_Category_Beneath()
        {
            // arrange
            var category = new Category() { Description = "Dogs for hunting foxes and badgers etc.", Id = 3, Name = "Hunting" };
            var beagle = new Breed() { Name = "Beagel", Category = category, Id = 3, Species = null };
            var bloodhound = new Breed() { Name = "Bloodhound", Category = category, Id = 3, Species = null };

            var beagleHuntingDog = new Dog() { Name = "Shep", Breed = beagle };
            var bloodhoundHuntingDog = new Dog() { Name = "Tip", Breed = bloodhound };
            var matchedDogs = new ObservableCollection<Dog>() { beagleHuntingDog, bloodhoundHuntingDog };

            _dogSearchhelper.Stub(x => x.GetSortedDogsList(3, SearchSortOptions.PRICE_HIGHEST)).Return(matchedDogs);

            var dogsController = new DogsController(_dogsRepository, _unitofWork, _dogSearchhelper);

            //act
            var result = dogsController.GetPaged(3, 1, 20, "Beagel", SearchSortOptions.PRICE_HIGHEST);

            Assert.That(result.Data.First(), Is.EqualTo(beagleHuntingDog));
            Assert.That(result.Data.Contains(bloodhoundHuntingDog));
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
            var dogsController = new DogsController(_dogsRepository, _unitofWork, _dogSearchhelper);
            const int page = 2;

            // act
            var result = dogsController.GetPaged(page, 4);

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
            // arrange
            var dogsController = new DogsController(_dogsRepository, _unitofWork, _dogSearchhelper);

            _dogsRepository.Stub(x => x.GetById(4)).Return(new Dog() { Name = "dog", Id = 4 });

            // act
            var result = dogsController.Get(4);

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