using NUnit.Framework;

namespace AnimalStore.Model.UnitTests
{
    [TestFixture]
    public class SpeciesTests
    {
        [Test]
        public void PropertyTests()
        {
            // arrange
            const int id = 5;
            const string name = "Dog";

            // act
            var species = new Species() { Id = id, Name = name };

            // assert 
            Assert.AreEqual(species.Id, id);
            Assert.AreEqual(species.Name, name);
        }
    }
}
