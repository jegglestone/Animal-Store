using NUnit.Framework;
using System;

namespace AnimalStore.Model.UnitTests
{
    [TestFixture]
    public class AnimalTests
    {
        [Test]
        public void PropertyTests()
        {
            // arrange
            const int id = 5;
            const string name = "Shep";
            const string desc = "A well behaved dog";
            const int age = 3;
            const bool isLitter = true;
            const bool isSold = false;
            const int price = 30;
            var breed = new Breed() { Id=1, Name="Alsatian" };
            var createdOn = new DateTime(2013, 2, 2);
            var modifiedOn = new DateTime(2013, 2, 4);

            // act
            var animal = new Animal
            {
                Id = id,
                Name = name,
                Headline = desc,
                AgeInYears = age,
                IsLitter = isLitter,
                IsSold = isSold,
                Price = price,
                Breed = breed,
                CreatedOn = createdOn,
                ModifiedOn = modifiedOn
            };

            // assert
            Assert.That(animal.Id, Is.EqualTo(id));
            Assert.That(animal.Name, Is.EqualTo(name));
            Assert.That(animal.Headline, Is.EqualTo(desc));
            Assert.That(animal.AgeInYears, Is.EqualTo(age));
            Assert.That(animal.IsLitter, Is.EqualTo(isLitter));
            Assert.That(animal.IsSold, Is.EqualTo(isSold));
            Assert.That(animal.Price, Is.EqualTo(price));
            Assert.That(animal.Breed, Is.EqualTo(breed));
            Assert.That(animal.CreatedOn, Is.EqualTo(createdOn));
            Assert.That(animal.ModifiedOn, Is.EqualTo(modifiedOn));
        }
    }
}
