using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Model.UnitTests
{
    [TestFixture]
    public class AnimalTests
    {
        [Test]
        public void PropertyTests()
        {
            // arrange
            var id = 5;
            var name = "Shep";
            var desc = "A well behaved dog";
            var age = 3;
            var isLitter = true;
            var isSold = false;
            var price = 30;
            var breed = new Breed() { Id=1, Name="Alsatian" };
            var createdOn = new DateTime(2013, 2, 2);
            var modifiedOn = new DateTime(2013, 2, 4);

            var animal = new Animal();

            // act
            animal.Id = id;
            animal.Name = name;
            animal.Desc = desc;
            animal.AgeInYears = age;
            animal.isLitter = isLitter;
            animal.isSold = isSold;
            animal.Price = price;
            animal.Breed = breed;
            animal.CreatedOn = createdOn;
            animal.ModifiedOn = modifiedOn;

            // assert
            Assert.That(animal.Id, Is.EqualTo(id));
            Assert.That(animal.Name, Is.EqualTo(name));
            Assert.That(animal.Desc, Is.EqualTo(desc));
            Assert.That(animal.AgeInYears, Is.EqualTo(age));
            Assert.That(animal.isLitter, Is.EqualTo(isLitter));
            Assert.That(animal.isSold, Is.EqualTo(isSold));
            Assert.That(animal.Price, Is.EqualTo(price));
            Assert.That(animal.Breed, Is.EqualTo(breed));
            Assert.That(animal.CreatedOn, Is.EqualTo(createdOn));
            Assert.That(animal.ModifiedOn, Is.EqualTo(modifiedOn));
        }
    }
}
