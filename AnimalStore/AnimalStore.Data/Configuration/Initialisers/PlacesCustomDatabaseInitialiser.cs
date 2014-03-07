using System.Data.Entity;

namespace AnimalStore.Data.Configuration.Initialisers
{
    public class PlacesCustomDatabaseInitialiser : 
        DropCreateDatabaseIfModelChanges<DataContext.PlacesDataContext>
    {
        protected override void Seed(DataContext.PlacesDataContext context)
        {
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSql0To3K);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSql3To5K);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSql5To7K);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSql7To10K);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSql10To13K);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSql13To16K);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSql16To19K);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSql19To22K);
            context.Database.ExecuteSqlCommand(PlacesSqlCommands.PlacesInsertSql22To25KAndRebuildIndex);

            base.Seed(context);
        }
    }
}
