using AnimalStore.Model;
using System.Data.Entity.ModelConfiguration;

namespace AnimalStore.Data.Configuration
{
    public class BreedConfiguration : EntityTypeConfiguration<Breed>
    {
        public BreedConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(70);
        }
    }
}
