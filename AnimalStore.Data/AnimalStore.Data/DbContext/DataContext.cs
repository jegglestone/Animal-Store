using System;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.Entity;
using AnimalStore.Model;

namespace AnimalStore.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Breed> Breeds { get; set; }

        public DataContext()
            :base(nameOrConnectionString: DataContext.ConnectionStringName) {}

        public static string ConnectionStringName
        {
            get
            {
                if (ConfigurationManager.AppSettings["ConnectionStringName"] != null)
                {
                    return ConfigurationManager.
                        AppSettings["ConnectionStringName"].ToString();
                }
                return "DefaultConnection";
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
