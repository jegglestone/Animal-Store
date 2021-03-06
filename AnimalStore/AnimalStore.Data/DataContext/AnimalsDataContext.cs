﻿using System;
using System.Linq;
using System.Data.Entity;
using AnimalStore.Model;
using AnimalStore.Data.Configuration;
using AnimalStore.Model.Interfaces;
using AnimalStore.Data.Helpers;
using AnimalStore.Data.Configuration.Initialisers;

namespace AnimalStore.Data.DataContext
{
    public class AnimalsDataContext : DbContext, IAnimalsDataContext
    {
        public IDbSet<Animal> Animals { get; set; }
        public IDbSet<Dog> Dogs { get; set; }
        public IDbSet<Species> Species { get; set; }
        public IDbSet<Breed> Breeds { get; set; }
        public IDbSet<Category> Categories { get; set; }

        public AnimalsDataContext()
            : base(ConnectionStringHelper.ConnectionStringName) { }

        static AnimalsDataContext()
        {
            Database.SetInitializer(new AnimalsCustomDatabaseInitialiser());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnimalConfiguration());
            modelBuilder.Configurations.Add(new BreedConfiguration());
            modelBuilder.Configurations.Add(new SpeciesConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
        }

        public override int SaveChanges()
        {
            ApplyRules();
            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var entityValidationError in e.EntityValidationErrors)
                {
                    var logManager = new Common.Logging.LogManager();
                    var log = logManager.GetLogger((typeof(AnimalsDataContext)));

                    log.Error("Entity Validation Error in configuring test data " + entityValidationError.Entry + ", " + entityValidationError.ValidationErrors, e);
                }

                throw e;
            }
        }

        private void ApplyRules()
        {
            foreach (var entry in ChangeTracker.Entries()
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

        public void SetModified(object entity)
        {
            throw new NotImplementedException();
        }

        public void SetAdd(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
