using AnimalStore.Model;
using System.Data.Entity.ModelConfiguration;

namespace AnimalStore.Data.Configuration
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            Property(p => p.Name).HasMaxLength(25);
            Property(p => p.Description).HasMaxLength(1000);
        }
    }
}
