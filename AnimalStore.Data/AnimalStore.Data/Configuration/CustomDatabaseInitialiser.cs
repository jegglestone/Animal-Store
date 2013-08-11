using AnimalStore.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace AnimalStore.Data.Configuration
{
    public class CustomDatabaseInitialiser : 
        DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            #region "Add some species to database"

            string[] speciesList = new string[2] {
                "Bird",
                "Cat"
            };

            int speciesCount = speciesList.Length + 1;
            while ((speciesCount--) != 0)
            {
                Species species = new Species();
                species.Name = speciesList[speciesCount];

                context.Species.Add(species);
            }

            #endregion

            #region "Add some breeds to database"

            string[] breedList = new string[5] {
                "German Shepard",
                "Rottweiler",
                "Bulldog",
                "Yorkshire Terrier",
                "Great Dane"
            };

            int breedCount = breedList.Length + 1;
            while ((breedCount--) != 0)
            {
                Breed breed = new Breed();
                breed.Name = breedList[breedCount];

                context.Breeds.Add(breed);
            }

            #endregion

            #region "Add some dog story data to database"

            Species dogSpecies = new Species { Name = "Dog"};
            Breed dalmatian = new Breed { Name = "Dalmatian", Species = dogSpecies };
            Breed goldenRetriever = new Breed { Name = "Golden Retriever", Species = dogSpecies };

            List<Animal> animals = new List<Animal>();
            context.Animals.Add(new Animal { Age = 4, Desc = "A well behaved dalmatian.", Name = "Jessie", isLitter = false, isSold = false, Breed = dalmatian });
            context.Animals.Add(new Animal { Age = 1, Desc = "A young Golden Retriever. Well behaved and trained.", Name = "Goldie", isLitter = false, isSold = false, Breed = goldenRetriever });

            #endregion

            base.Seed(context);
        }
    }
}
