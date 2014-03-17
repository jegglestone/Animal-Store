using AnimalStore.Data.Configuration;
using AnimalStore.Data.Configuration.Initialisers;
using AnimalStore.Data.Helpers;
using AnimalStore.Model;
using System.Configuration;
using System.Data.Entity;

namespace AnimalStore.Data.DataContext
{
    public class PlacesDataContext : DbContext
    {
        public IDbSet<Place> Places { get; set; }

        public PlacesDataContext()
            : base(ConnectionStringHelper.PlacesConnectionStringName) { }


        static PlacesDataContext()
        {
            Database.SetInitializer(new PlacesCustomDatabaseInitialiser());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PlaceConfiguration());
        }
    }
}
