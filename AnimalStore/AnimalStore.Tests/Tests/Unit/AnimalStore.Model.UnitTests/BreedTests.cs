using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Model.UnitTests
{
    [TestFixture]
    public class BreedTests
    {
        [Test]
        public void PropertyTests()
        {
            // arrange
            var id = 5;
            var name = "Dalmatian";
            var species = new Species() { Id = 1, Name = "Dog" };

            // act
            var breed = new Breed() { Id = id, Name = name, Species = species };

            // assert 
            Assert.AreEqual(breed.Id, id);
            Assert.AreEqual(breed.Name, name);
            Assert.AreEqual(breed.Species, species);
        }

    }
}
