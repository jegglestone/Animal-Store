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
                new Dog() { Name = "dog1" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },

                new Dog() { Name = "dog", CreatedOn = DateTime.Today },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },

                new Dog() { Name = "dog2"},
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },

                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },

                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },

                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
                new Dog() { Name = "dog2" },
            };
            _repository.Stub(x => x.GetAll()).Return(animalsListWith30Items.AsQueryable());
        }

        [Test]
        public void Get_CallRepositoryGetAllMethod()
        {
            // arrange
             var dogsController = new DogsController(_repository, _unitofWork);

            // act
            dogsController.Get();

            // assert
            _repository.AssertWasCalled(X => X.GetAll());
        }

        [Test]
        public void Get_ReturnsUpTo25Items()
        {
            // arrange
            var dogsController = new DogsController(_repository, _unitofWork);

            // act
            var result = dogsController.Get();

            // assert
            Assert.That(result.Count(), Is.EqualTo(25));
        }

        [Test]
        public void Get_ReturnsItemsOrderedByDateCreatedDescending()
        {
            // arrange
            var dogsController = new DogsController(_repository, _unitofWork);

            // act
            var result = dogsController.Get();

            // assert
            Assert.That(result.ToList()[0].CreatedOn, Is.EqualTo(DateTime.Today));
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
