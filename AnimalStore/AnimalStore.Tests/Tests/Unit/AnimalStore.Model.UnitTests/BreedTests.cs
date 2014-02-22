using NUnit.Framework;

namespace AnimalStore.Model.UnitTests
{
    [TestFixture]
    public class BreedTests
    {
        [Test]
        public void PropertyTests()
        {
            // arrange
            const int id = 5;
            const string name = "Dalmatian";
            var species = new Species() { Id = 1, Name = "Dog" };
            var category = new Category() { Id = 5, Name = "Guard dog" };

            // act
            var breed = new Breed() { Id = id, Name = name, Species = species, Category = category };

            // assert 
            Assert.AreEqual(breed.Id, id);
            Assert.AreEqual(breed.Name, name);
            Assert.AreEqual(breed.Species, species);
            Assert.AreEqual(breed.Category, category);
        }
    }
}
