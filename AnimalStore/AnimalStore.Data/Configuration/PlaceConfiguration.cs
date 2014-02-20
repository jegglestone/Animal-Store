using AnimalStore.Model;
using System.Data.Entity.ModelConfiguration;

namespace AnimalStore.Data.Configuration
{
    public class PlaceConfiguration : EntityTypeConfiguration<Place>
    {
        public PlaceConfiguration()
        {
            Property(p => p.Id).IsRequired();
            Property(p => p.Name).IsRequired().HasMaxLength(255);
            Property(p => p.AltName).HasMaxLength(255);
            Property(p => p.County).HasMaxLength(255);
            Property(p => p.Country).HasMaxLength(255);
        }
    }
}
