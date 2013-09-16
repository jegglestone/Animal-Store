using AnimalStore.Model;
using System.Data.Entity;

namespace AnimalStore.Data.Configuration
{
    public class CustomDatabaseInitialiser : 
        DropCreateDatabaseIfModelChanges<DataContext.AnimalsDataContext>
    {
        protected override void Seed(DataContext.AnimalsDataContext context)
        {
             #region "Add some dog story data to database"

            // species
            var dogSpecies = new Species { Name = "Dog" };
            context.Species.Add(dogSpecies);

            // breeds
            var bloodhound = new Breed { Name = "Blood-hound", Species = dogSpecies };
            var dalmatian = new Breed { Name = "Dalmatian", Species = dogSpecies };
            var goldenRetriever = new Breed { Name = "Golden Retriever", Species = dogSpecies };

            context.Breeds.Add(bloodhound);
            context.Breeds.Add(dalmatian);
            context.Breeds.Add(goldenRetriever);

            // individual animals
            context.Animals.Add(new Animal { AgeInYears = 4, Desc = "A well behaved dalmatian.", Name = "Jessie", isLitter = false, isSold = false, Breed = dalmatian });
            context.Animals.Add(new Animal { AgeInYears = 1, Desc = "A young Golden Retriever. Well behaved and trained.", Name = "Goldie", isLitter = false, isSold = false, Breed = goldenRetriever });
            context.Animals.Add(new Animal { AgeInYears = 0, Desc = "6 brand new dalmations. Pedigree standard, all healthy and fit. Ready in 5 weeks after wheaning", Name = null, isLitter = true, isSold = false, Breed = dalmatian });
            context.Animals.Add(new Animal { AgeInYears = 2, Desc = "2 yr old Blood-hound. Well-behaved, likes children", Name = "Spud", isLitter = false, isSold = false, Breed = bloodhound });

            #endregion

            base.Seed(context);
        }
    }
}
