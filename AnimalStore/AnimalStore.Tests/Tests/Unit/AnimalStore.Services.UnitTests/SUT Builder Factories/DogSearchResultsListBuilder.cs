using AnimalStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Services.UnitTests.SUT_Builder_Factories
{
    public class DogSearchResultsListBuilder
    {
        private List<Dog> dogs;

        internal DogSearchResultsListBuilder()
        {
            dogs = new List<Dog>();
        }

        internal DogSearchResultsListBuilder ListWith30Dogs()
        {
            var animalsListWith30Items = new List<Dog>()
            {
                new Dog() { Name = "dog1", CreatedOn = DateTime.Today.AddHours(-1), Id=1 },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-1), Id=2 },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-1), Id=3 },
                new Dog() { Name = "dog2", CreatedOn = DateTime.Today.AddHours(-1), Id=4 },
                new Dog() { Name = "Flossie", CreatedOn = DateTime.Today.AddHours(-1), Id=5 },

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
            dogs.AddRange(animalsListWith30Items);
            return this;
        }

        internal DogSearchResultsListBuilder ListOf14Beagels()
        {
            var category = new Category() { Description = "Dogs for hunting foxes and badgers etc.", Id = 3, Name = "Hunting" };
            var beagle = new Breed() { Name = "Beagel", Category = category, Id = 3, Species = null };

            var beagleHuntingDog = new Dog() { Name = "Shep", Breed = beagle };
            var fourteenMatchedDogs = new ObservableCollection<Dog>() 
            {   beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog
                ,beagleHuntingDog            
            };
            dogs.AddRange(fourteenMatchedDogs);
            return this;
        }

        internal DogSearchResultsListBuilder WithAnotherDog(Dog dog)
        {
            dogs.Add(dog);
            return this;
        }

        internal List<Dog> Build()
        {
            return dogs;
        }
    }
}
