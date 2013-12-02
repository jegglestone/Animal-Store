using AnimalStore.Web.API.Controllers;
using AnimalStore.Data.Repositories;
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
        private readonly IRepository<Dog> _repository;
        private readonly IUnitOfWork _unitofWork;

        public DogsControllerTests ()
	    {
            _repository = MockRepository.GenerateMock<IRepository<Dog>>();
            _unitofWork = MockRepository.GenerateMock<IUnitOfWork>();

            StubRepositoryGetAll();
	    }

        private void StubRepositoryGetAll()
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
            _repository.Stub(x => x.GetAll()).Return(animalsListWith30Items.AsQueryable());
        }

        [Test]
        public void Get_CallRepositoryGetAllMethod()
        {
            // arrange
             var dogsController = new DogsController(_repository, _unitofWork);

            // act
            dogsController.GetPaged();

            // assert
            _repository.AssertWasCalled(X => X.GetAll());
        }

        [Test]
        public void Get_ReturnsUpTo25Items_WithNoPageLimitSpecified()
        {
            // arrange
            var dogsController = new DogsController(_repository, _unitofWork);

            // act
            var result = dogsController.GetPaged();

            // assert
            Assert.That(result.Data.Count(), Is.EqualTo(25));
        }

        [Test]
        public void Get_ReturnsSpecifiedNumberOfResultSetWhenPaging()
        {
            // arrange
            var dogsController = new DogsController(_repository, _unitofWork);

            // act
            var result = dogsController.GetPaged(1, 10);

            // assert
            Assert.That(result.Data.Count(), Is.EqualTo(10));
        }

        [TestCase(1, 10, "Flossie", Description="we expect the first page to have this dog")]
        [TestCase(2, 10, "Rex", Description = "we expect the second page to have this dog")]
        [TestCase(3, 10, "Tip", Description = "we expect the third page to have this dog")]
        public void get_ReturnsTheSpecifiedPage(int pageNumber, int pageSize, string expectedDogName)
        {
             // arrange
            var dogsController = new DogsController(_repository, _unitofWork);

            // act
            var result = dogsController.GetPaged(pageNumber, pageSize);

            // assert
            Assert.That(result.Data.ToList().First(dog => dog.Name == expectedDogName), Is.Not.Null);
        }

        [Test]
        public void Get_ReturnsTheCorrectTotalCount()
        {
            // arrange
            var dogsController = new DogsController(_repository, _unitofWork);

            // act
            var result = dogsController.GetPaged(1, 10);

            // assert
            Assert.That(result.TotalCount, Is.EqualTo(30));
        }

        [Test]
        public void Get_ReturnsTheCorrectPageCount()
        {
            // arrange
            var dogsController = new DogsController(_repository, _unitofWork);

            // act
            var result = dogsController.GetPaged(1, 9);

            // assert
            Assert.That(result.TotalPages, Is.EqualTo(4));
        }

        [Test]
        public void Get_ReturnsTheCurrentPage()
        {
            // arrange
            var dogsController = new DogsController(_repository, _unitofWork);
            int page = 2;

            // act
            var result = dogsController.GetPaged(page, 4);

            // assert
            Assert.That(result.CurrentPageNumber, Is.EqualTo(page));
        }

        [Test]
        public void Get_ReturnsTheCorrectNextPageUrl()
        {
            // arrange
            var dogsController = new DogsController(_repository, _unitofWork);

            // act
            var result = dogsController.GetPaged(1, 10);

            // assert
            Assert.That(result.NextPage.Contains("?page=2"));
        }

        [Test]
        public void Get_ReturnsTheCorrectPrevPageUrl()
        {
            // arrange
            var dogsController = new DogsController(_repository, _unitofWork);

            // act
            var result = dogsController.GetPaged(2, 10);

            // assert
            Assert.That(result.PrevPage.Contains("?page=1"));
        }

        [Test]
        public void Get_ReturnsItemsOrderedByDateCreatedDescending()
        {
            // arrange
            var dogsController = new DogsController(_repository, _unitofWork);

            // act
            var result = dogsController.GetPaged(0, 20);

            // assert
            Assert.That(result.Data.ToList()[0].CreatedOn, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void Get_ById_ReturnsSingleItemWithMatchingIdAndCallsRepositoryGetById()
        {
             // arrange
            var dogsController = new DogsController(_repository, _unitofWork);

            _repository.Stub(x => x.GetById(4)).Return(new Dog() { Name = "dog", Id = 4 });

            // act
            var result = dogsController.Get(4);

            // assert
            _repository.AssertWasCalled(x => x.GetById(4));
            Assert.That(result.Id, Is.EqualTo(4));
        }

        [TearDown]
        public void TearDown()
        {
            _repository.Dispose();
            _unitofWork.Dispose();
        }
    }
}
