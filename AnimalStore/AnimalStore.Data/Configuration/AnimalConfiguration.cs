using AnimalStore.Model;
using System.Data.Entity.ModelConfiguration;

namespace AnimalStore.Data.Configuration
{
    public class AnimalConfiguration : EntityTypeConfiguration<Animal> 
    {
        public AnimalConfiguration()
        {
            Property(p => p.isLitter).IsRequired();
            Property(p => p.isSold).IsRequired();
            Property(p => p.AgeInYears).IsRequired();
            Property(p => p.AgeInMonth).IsOptional();
            Property(p => p.Name).HasMaxLength(30);
            Property(p => p.Desc).HasMaxLength(300);
            Property(p => p.Price).IsOptional();
        }
    }
}
