using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Model.UnitTests
{
    [TestFixture]
    public class SpeciesTests
    {
        [Test]
        public void PropertyTests()
        {
            // arrange
            var id = 5;
            var name = "Dog";

            // act
            var species = new Species() { Id = id, Name = name };

            // assert 
            Assert.AreEqual(species.Id, id);
            Assert.AreEqual(species.Name, name);
        }
    }
}
