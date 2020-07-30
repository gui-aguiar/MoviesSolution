using System.Data.Entity.ModelConfiguration;
using Movies.Models;

namespace Movies.Database.Configuration
{
    public class GenderConfiguration : EntityTypeConfiguration<Gender>
    {
        public GenderConfiguration()
        {
            this.HasKey(g => g.Id)
                .Property(g => g.Id)
                .IsRequired();

            this.Property(g => g.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
