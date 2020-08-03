using Movies.Models;
using System.Data.Entity.ModelConfiguration;

namespace Movies.Database.Configuration
{
    /// <summary>
    /// Entity Framework Class responsible to configure the Rental Table of the database
    /// All the constraints and relevant configuration must be explicitly defined by it.
    /// </summary>
    public class RentalConfiguration : EntityTypeConfiguration<Rental>
    {
        public RentalConfiguration()
        {
            this.HasKey(r => r.Id)
                .Property(r => r.Id)
                .IsRequired();

            this.Property(r => r.CustomerCPF)
                .HasMaxLength(14);

            this.Property(r => r.RentalDateTime)
                .IsRequired();

            this.HasMany<Movie>(m => m.MoviesList);          
        }
    }
}
