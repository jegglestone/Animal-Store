using AnimalStore.Data.Configuration;
using AnimalStore.Model;
using System.Configuration;
using System.Data.Entity;

namespace AnimalStore.Data.DataContext
{
    public class PlacesDataContext : DbContext
    {
        public IDbSet<Place> Places { get; set; }

        //public PlacesDataContext()
        //    :base(ConnectionStringName) {}

        public PlacesDataContext()
            : base("Data Source=.\\SQLEXPRESS;Initial Catalog=Places;Integrated Security=True") { }  // TODO: Why can't it pull this from the web.config?

        static PlacesDataContext()
        {
            Database.SetInitializer(new PlacesCustomDatabaseInitialiser());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PlaceConfiguration());
        }

        private static string ConnectionStringName
        {
            get
            {
                return ConfigurationManager.AppSettings["PlacesContextConnectionString"];
            }
        }
    }
}
