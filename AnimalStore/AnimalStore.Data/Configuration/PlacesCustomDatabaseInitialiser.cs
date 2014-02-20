using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Data.Configuration
{
    public class PlacesCustomDatabaseInitialiser : 
        DropCreateDatabaseIfModelChanges<DataContext.PlacesDataContext>
    {
        protected override void Seed(DataContext.PlacesDataContext context)
        {
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSQL0To3k);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSQL3To5k);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSQL5To7k);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSQL7To10k);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSQL10To13k);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSQL13To16k);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSQL16To19k);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSQL19To22k);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSQL22To25kAndRebuildIndex);

            base.Seed(context);
        }
    }
}
