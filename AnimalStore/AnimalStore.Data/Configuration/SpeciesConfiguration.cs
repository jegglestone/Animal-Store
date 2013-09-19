using AnimalStore.Model;
using System.Data.Entity.ModelConfiguration;

namespace AnimalStore.Data.Configuration
{
    public class SpeciesConfiguration : EntityTypeConfiguration<Species>
    {
        public SpeciesConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(30);
        }
    }
}
