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
             #region "Add some dog story data to database"

            Species dogSpecies = new Species { Name = "Dog"};
            Breed dalmatian = new Breed { Name = "Dalmatian", Species = dogSpecies };
            Breed goldenRetriever = new Breed { Name = "Golden Retriever", Species = dogSpecies };

            context.Species.Add(dogSpecies);
            context.Breeds.Add(dalmatian);
            context.Breeds.Add(goldenRetriever);

            context.Animals.Add(new Animal { Age = 4, Desc = "A well behaved dalmatian.", Name = "Jessie", isLitter = false, isSold = false, Breed = dalmatian });
            context.Animals.Add(new Animal { Age = 1, Desc = "A young Golden Retriever. Well behaved and trained.", Name = "Goldie", isLitter = false, isSold = false, Breed = goldenRetriever });

            #endregion

            base.Seed(context);
        }
    }
}
