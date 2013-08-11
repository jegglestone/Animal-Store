using AnimalStore.Model;
using System.Data.Entity.ModelConfiguration;

namespace AnimalStore.Data.Configuration
{
    public class BreedConfiguration : EntityTypeConfiguration<Breed>
    {
        public BreedConfiguration()
        {
            this.Property(p => p.Name).IsRequired().HasMaxLength(30);
        }
    }
}
