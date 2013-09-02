using AnimalStore.Model;
using System.Data.Entity;

namespace AnimalStore.Data.Configuration
{
    public class CustomDatabaseInitialiser : 
        DropCreateDatabaseIfModelChanges<DataContext.DataContext>
    {
        protected override void Seed(DataContext.DataContext context)
        {
             #region "Add some dog story data to database"

            var dogSpecies = new Species { Name = "Dog"};
            var dalmatian = new Breed { Name = "Dalmatian", Species = dogSpecies };
            var goldenRetriever = new Breed { Name = "Golden Retriever", Species = dogSpecies };

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
