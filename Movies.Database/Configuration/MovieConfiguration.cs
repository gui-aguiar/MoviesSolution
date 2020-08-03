using Movies.Models;
using System.Data.Entity.ModelConfiguration;


namespace Movies.Database.Configuration
{
    /// <summary>
    /// Entity Framework Class responsible to configure the Movie Table of the database
    /// All the constraints and relevant configuration must be explicitly defined by it.
    /// </summary>
    public class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {
            this.HasKey(m => m.Id)
                .Property(m => m.Id)
                .IsRequired();

            this.Property(m => m.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
