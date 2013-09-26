using AnimalStore.Data.UnitTests.Fakes;
using AnimalStore.Data.UnitsOfWork;
using NUnit.Framework;

namespace AnimalStore.Data.UnitTests
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        [Test]
        public void Save_Saves()
        {
            // arrange
            var uow = new UnitOfWork<FakeAnimalsDbContext>();

            // act
            var result = uow.Save();

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Context_RetrievesContext()
        {
            // arrange/act
            var uow = new UnitOfWork<FakeAnimalsDbContext>();

            // act
           Assert.That(uow.Context, Is.TypeOf<FakeAnimalsDbContext>());
        }
    }
}
