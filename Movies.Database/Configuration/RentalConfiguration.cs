using Movies.Models;
using System.Data.Entity.ModelConfiguration;

namespace Movies.Database.Configuration
{
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

            this.HasMany<Movie>(m => m.MoviesList)
                .WithRequired();
        }
    }
}
