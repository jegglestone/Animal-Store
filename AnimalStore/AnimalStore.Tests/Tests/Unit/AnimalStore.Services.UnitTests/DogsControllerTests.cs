using System.Collections.ObjectModel;
using AnimalStore.Web.API.Controllers;
using AnimalStore.Data.Repositories;
using AnimalStore.Web.API.Strategies;
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
        private readonly IRepository<Breed> _breedsRepository;
        private readonly IDogBreedFilterStrategy _dogBreedFilterStrategy;
        private readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;
        private readonly IUnitOfWork _unitofWork;


        public DogsControllerTests()
        {
            _dogsRepository = MockRepository.GenerateMock<IRepository<Dog>>();
            _breedsRepository = MockRepository.GenerateMock<IRepository<Breed>>();
            _unitofWork = MockRepository.GenerateMock<IUnitOfWork>();

            _dogBreedFilterStrategy = MockRepository.GenerateMock<IDogBreedFilterStrategy>();
            _dogCategoryFilterStrategy = MockRepository.GenerateMock<IDogCategoryFilterStrategy>();

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
        public void Get_Paged_With_Breed_ReturnsDogs_In_BreedCategory_When_There_Are_None_Matching_Exact_Breed()
        {
            // arrange
            var category = new Category() { Description = "Dogs for hunting foxes and badgers etc.", Id = 3, Name = "Hunting" };
            var beagle = new Breed() { Name = "Beagel", Category = category, Id = 3, Species = null };
            var bloodhound = new Breed() { Name = "Bloodhound", Category = category, Id = 3, Species = null };
            var breeds = new List<Breed>() { beagle, bloodhound }.AsEnumerable();

            category.Breeds = (ICollection<Breed>)breeds;

            var bloodhoundHuntingDog = new Dog() { Name = "Tip", Breed = bloodhound };
            var sameCategoryDogs = new ObservableCollection<Dog>() { bloodhoundHuntingDog };

            _dogBreedFilterStrategy.Expect(action => action.Filter(3, dog => dog.CreatedOn))
                .IgnoreArguments().Return(null);

            _dogCategoryFilterStrategy.Expect(action => action.Filter(3, dog => dog.CreatedOn))
                .IgnoreArguments().Return(sameCategoryDogs);

            _breedsRepository.Stub(x => x.GetById(Arg<int>.Is.Anything))
                             .Return(beagle);

            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);

            //act
            var result = dogsController.GetPaged(3, 1, 20);

            Assert.That(result.Data.Contains(bloodhoundHuntingDog));
        }

        [Test]
        public void Get_Paged_With_Breed_Works_When_There_Are_No_Other_Dogs_In_The_Same_Category()
        {
            // arrange
            var category = new Category() { Description = "Dogs for hunting foxes and badgers etc.", Id = 3, Name = "Hunting" };
            var beagle = new Breed() { Name = "Beagel", Category = category, Id = 3, Species = null };
            var bloodhound = new Breed() { Name = "Bloodhound", Category = category, Id = 3, Species = null };

            var breeds = new List<Breed>() { beagle, bloodhound }.AsEnumerable();
            category.Breeds = (ICollection<Breed>)breeds;

            var beagleHuntingDog = new Dog() { Name = "Shep", Breed = beagle };
            var matchingDogs = new ObservableCollection<Dog>() { beagleHuntingDog };

            var dogBreedFilterStrategy = MockRepository.GenerateMock<IDogBreedFilterStrategy>();
            dogBreedFilterStrategy.Expect(action => action.Filter(3, dog => dog.CreatedOn))
                .IgnoreArguments().Return(matchingDogs);

            var dogCategoryFilterStrategy = MockRepository.GenerateMock<IDogCategoryFilterStrategy>();
            dogCategoryFilterStrategy.Expect(action => action.Filter(3, dog => dog.CreatedOn))
                .IgnoreArguments().Return(null);

            _breedsRepository.Stub(x => x.GetById(Arg<int>.Is.Anything))
                             .Return(beagle);

            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, dogBreedFilterStrategy, dogCategoryFilterStrategy);

            //act
            var result = dogsController.GetPaged(3, 1, 20);

            Assert.That(result.Data.First(), Is.EqualTo(beagleHuntingDog));
        }

        [Test]
        public void Get_Paged_With_Breed_Returns_MatchingDogs_And_Dogs_In_The_Same_Category_Beneath()
        {
            // arrange
            var category = new Category() { Description = "Dogs for hunting foxes and badgers etc.", Id = 3, Name = "Hunting" };
            var beagle = new Breed() { Name = "Beagel", Category = category, Id = 3, Species = null };
            var bloodhound = new Breed() { Name = "Bloodhound", Category = category, Id = 3, Species = null };
            var breeds = new List<Breed>() { beagle, bloodhound }.AsEnumerable();

            category.Breeds = (ICollection<Breed>)breeds;

            var beagleHuntingDog = new Dog() { Name = "Shep", Breed = beagle };
            var matchedDogs = new ObservableCollection<Dog>() { beagleHuntingDog };
            var bloodhoundHuntingDog = new Dog() { Name = "Tip", Breed = bloodhound };
            var sameCategoryDogs = new ObservableCollection<Dog>() { bloodhoundHuntingDog };

            var dogBreedFilterStrategy = MockRepository.GenerateMock<IDogBreedFilterStrategy>();
            dogBreedFilterStrategy.Expect(action => action.Filter(3, dog => dog.CreatedOn))
                .IgnoreArguments().Return(matchedDogs);

            var dogCategoryFilterStrategy = MockRepository.GenerateMock<IDogCategoryFilterStrategy>();
            dogCategoryFilterStrategy.Expect(action => action.Filter(3, dog => dog.CreatedOn))
                .IgnoreArguments().Return(sameCategoryDogs);

            var breedRepository = MockRepository.GenerateMock<IRepository<Breed>>();
            breedRepository.Stub(x => x.GetById(Arg<int>.Is.Anything))
                             .Return(beagle);

            var dogsController = new DogsController(_dogsRepository, breedRepository, _unitofWork, dogBreedFilterStrategy, dogCategoryFilterStrategy);

            //act
            var result = dogsController.GetPaged(3, 1, 20);

            Assert.That(result.Data.First(), Is.EqualTo(beagleHuntingDog));
            Assert.That(result.Data.Contains(bloodhoundHuntingDog));
        }

        [Test]
        public void Get_CallRepositoryGetAllMethod()
        {
            // arrange
            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);

            // act
            dogsController.GetPaged();

            // assert
            _dogsRepository.AssertWasCalled(X => X.GetAll());
        }

        [Test]
        public void Get_ReturnsUpTo25Items_WithNoPageLimitSpecified()
        {
            // arrange
            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);

            // act
            var result = dogsController.GetPaged();

            // assert
            Assert.That(result.Data.Count(), Is.EqualTo(25));
        }

        [Test]
        public void Get_ReturnsSpecifiedNumberOfResultSetWhenPaging()
        {
            // arrange
            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);

            // act
            var result = dogsController.GetPaged(1, 10);

            // assert
            Assert.That(result.Data.Count(), Is.EqualTo(10));
        }

        [TestCase(1, 10, "Flossie", Description = "we expect the first page to have this dog")]
        [TestCase(2, 10, "Rex", Description = "we expect the second page to have this dog")]
        [TestCase(3, 10, "Tip", Description = "we expect the third page to have this dog")]
        public void get_ReturnsTheSpecifiedPage(int pageNumber, int pageSize, string expectedDogName)
        {
            // arrange
            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);

            // act
            var result = dogsController.GetPaged(pageNumber, pageSize);

            // assert
            Assert.That(result.Data.ToList().First(dog => dog.Name == expectedDogName), Is.Not.Null);
        }

        [Test]
        public void Get_ReturnsTheCorrectTotalCount()
        {
            // arrange
            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);

            // act
            var result = dogsController.GetPaged(1, 10);

            // assert
            Assert.That(result.TotalCount, Is.EqualTo(30));
        }

        [Test]
        public void Get_ReturnsTheCorrectPageCount()
        {
            // arrange
            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);

            // act
            var result = dogsController.GetPaged(1, 9);

            // assert
            Assert.That(result.TotalPages, Is.EqualTo(4));
        }

        [Test]
        public void Get_ReturnsTheCurrentPage()
        {
            // arrange
            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);
            const int page = 2;

            // act
            var result = dogsController.GetPaged(page, 4);

            // assert
            Assert.That(result.CurrentPageNumber, Is.EqualTo(page));
        }

        [Test]
        public void Get_ReturnsTheCorrectNextPageUrl()
        {
            // arrange
            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);

            // act
            var result = dogsController.GetPaged(1, 10);

            // assert
            Assert.That(result.NextPage.Contains("?page=2"));
        }

        [Test]
        public void Get_ReturnsTheCorrectPrevPageUrl()
        {
            // arrange
            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);

            // act
            var result = dogsController.GetPaged(2, 10);

            // assert
            Assert.That(result.PrevPage.Contains("?page=1"));
        }

        [Test]
        public void Get_ReturnsItemsOrderedByDateCreatedDescending()
        {
            // arrange
            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);

            // act
            var result = dogsController.GetPaged(0, 20);

            // assert
            Assert.That(result.Data.ToList()[0].CreatedOn, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void Get_ById_ReturnsSingleItemWithMatchingIdAndCallsRepositoryGetById()
        {
            // arrange
            var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogBreedFilterStrategy, _dogCategoryFilterStrategy);

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