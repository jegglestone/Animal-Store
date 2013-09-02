using System.Data.Entity;
using AnimalStore.Data.Repositories;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnimalStore.Data.UnitTests
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        [SetUp]
        public void SetUp()
        {
            
         
        }

        //[Test]
        //public void GenericRepositoryConstructor_ThrowsCorrectExceptionWhenNoDbContextIsInjected()
        //{
        //    //act
        //    var ex = Assert.Throws<Exception>(() => new GenericRepository<Animal>(null));
        //    Assert.That(ex.Message, Is.EqualTo("context"));
        //    Assert.That(ex.InnerException, Is.EqualTo("An instance of DbContext is required to use this generic repository"));
        //}

        [Test]
        public void GetAll_ReturnsIQueryableT()
        {
        }

        [Test]
        public void GetById_ReturnsObjectOfTypeT()
        {
        }

        [Test]
        public void Add_AddsObjectT()
        {
        }

        [Test]
        public void Delete_DeletesObjectT()
        {
        }
    }
}
