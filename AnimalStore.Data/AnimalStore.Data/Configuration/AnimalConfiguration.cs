using AnimalStore.Model;
using System.Data.Entity.ModelConfiguration;

namespace AnimalStore.Data.Configuration
{
    public class AnimalConfiguration : EntityTypeConfiguration<Animal> 
    {
        public AnimalConfiguration()
        {
            this.Property(p => p.isLitter).IsRequired();
            this.Property(p => p.isSold).IsRequired();
            this.Property(p => p.Age).IsRequired();
            this.Property(p => p.Name).HasMaxLength(30);
            this.Property(p => p.Desc).HasMaxLength(300);
        }
    }
}
