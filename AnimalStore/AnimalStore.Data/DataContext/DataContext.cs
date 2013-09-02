using System;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.Entity;
using AnimalStore.Model;
using AnimalStore.Data.Configuration;
using AnimalStore.Model.Interfaces;

namespace AnimalStore.Data.DataContext
{
    public class DataContext : DbContext
    {
        public IDbSet<Animal> Animals { get; set; }
        public IDbSet<Species> Species { get; set; }
        public IDbSet<Breed> Breeds { get; set; }

        public DataContext()
            :base(ConnectionStringName) {}

        static DataContext()
        {
            Database.SetInitializer(new CustomDatabaseInitialiser());
        }

        private static string ConnectionStringName
        {
            get
            {
                if (ConfigurationManager.AppSettings["ConnectionStringName"] != null)
                {
                    return ConfigurationManager.
                        AppSettings["ConnectionStringName"];
                }
                return "DefaultConnection";
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnimalConfiguration());
            modelBuilder.Configurations.Add(new BreedConfiguration());
            modelBuilder.Configurations.Add(new SpeciesConfiguration());
        }

        public override int SaveChanges()
        {
            ApplyRules();
            return base.SaveChanges();
        }

        private void ApplyRules()
        {
            foreach (var entry in this.ChangeTracker.Entries()
                        .Where (
                            e => e.Entity is IAuditInfo &&
                            (e.State==EntityState.Added) ||
                            (e.State==EntityState.Modified)))
            {
                var e = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    e.CreatedOn = DateTime.Now;
                }

                e.ModifiedOn = DateTime.Now;
            }
        }
    }
}
