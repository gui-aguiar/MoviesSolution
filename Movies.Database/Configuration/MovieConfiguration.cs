using Movies.Models;
using System.Data.Entity.ModelConfiguration;


namespace Movies.Database.Configuration
{
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
