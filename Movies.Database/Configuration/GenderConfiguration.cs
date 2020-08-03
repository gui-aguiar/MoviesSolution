using System.Data.Entity.ModelConfiguration;
using Movies.Models;

namespace Movies.Database.Configuration
{
    /// <summary>
    /// Entity Framework Class responsible to configure the Gender Table of the database
    /// All the constraints and relevant configuration must be explicitly defined by it.
    /// </summary>
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
