using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AnimalStore.Model;

namespace AnimalStore.Services.UnitTests
{
    public class DogSearchResultsListBuilder
    {
        private readonly List<Dog> _dogs;

        internal DogSearchResultsListBuilder()
        {
            _dogs = new List<Dog>();
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
            _dogs.AddRange(animalsListWith30Items);
            return this;
        }

        internal DogSearchResultsListBuilder ListOf3DogsWithConfigurableLocation(int breedId, int placeId1, int placeId2, int placeId3)
        {
            var dogs = new List<Dog>()
            {
                new Dog() {BreedId = breedId, PlaceId = placeId1},
                new Dog() {BreedId = breedId, PlaceId = placeId2},
                new Dog() {BreedId = breedId, PlaceId = placeId3},
            };
            _dogs.AddRange((dogs));
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
            _dogs.AddRange(fourteenMatchedDogs);
            return this;
        }

        internal DogSearchResultsListBuilder ListOf3DalmatiansByCategory(int categoryId, int breedId)
        {
            var category = new Category() { Description = "Dogs for hunting foxes and badgers etc.", Id = categoryId, Name = "Hunting" };
            var dalmatian = new Breed() { Name = "Dalmatian", Category = category, Id = breedId, Species = null };

            var dalmatianDog1 = new Dog() { Name = "Tip", Breed = dalmatian, BreedId = breedId};
            var dalmatianDog2 = new Dog() { Name = "Snoop", Breed = dalmatian, BreedId = breedId };
            var dalmatianDog3 = new Dog() { Name = "Ron", Breed = dalmatian, BreedId = breedId };

            var matchedDogs = new ObservableCollection<Dog>() 
            {   dalmatianDog1
                ,dalmatianDog2
                ,dalmatianDog3
            };
            _dogs.AddRange(matchedDogs);
            return this;
        }

        internal DogSearchResultsListBuilder ListOfThreeDuplicateDogs(int categoryId, int breedId)
        {
            var category = new Category() { Description = "Dogs for hunting foxes and badgers etc.", Id = categoryId, Name = "Hunting" };
            var dalmatian = new Breed() { Name = "Dalmatian", Category = category, Id = breedId, Species = null };
            var dalmatianDog = new Dog() { Name = "Tip", Breed = dalmatian };

            var matchedDogs = new ObservableCollection<Dog>() 
            {   dalmatianDog
                ,dalmatianDog
                ,dalmatianDog
            };
            _dogs.AddRange(matchedDogs);
            return this;
        }

        internal DogSearchResultsListBuilder ListOf3BeagelsByCategory(int categoryId, int breedId)
        {
            var category = new Category() { Description = "Dogs for hunting foxes and badgers etc.", Id = categoryId, Name = "Hunting" };
            var beagle = new Breed() { Name = "Beagel", Category = category, Id = breedId, Species = null };

            var beagleHuntingDog1 = new Dog() { Name = "Shep", Breed = beagle };
            var beagleHuntingDog2 = new Dog() { Name = "Flo", Breed = beagle };
            var beagleHuntingDog3 = new Dog() { Name = "Rex", Breed = beagle };

            var matchedDogs = new ObservableCollection<Dog>() 
            {   beagleHuntingDog1
                ,beagleHuntingDog2
                ,beagleHuntingDog3
            };
            _dogs.AddRange(matchedDogs);
            return this;
        }

        internal DogSearchResultsListBuilder WithAnotherDog(Dog dog)
        {
            _dogs.Add(dog);
            return this;
        }

        internal List<Dog> Build()
        {
            return _dogs;
        }
    }
}
