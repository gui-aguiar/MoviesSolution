using System.Data.Entity;
using Movies.Models;
using Movies.Database.Configuration;

namespace Movies.Database
{
    public class MoviesDBContext : DbContext
    {
        public MoviesDBContext() : base(nameOrConnectionString: "MoviesDB")
        {
            System.Data.Entity.Database.SetInitializer<MoviesDBContext>(new MoviesDBInitializer());

            #if DEBUG
            Database.Log = System.Console.Write;
            #endif
        }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Rental> Rental { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Movies");
            modelBuilder.Configurations.Add(new GenderConfiguration());
            modelBuilder.Configurations.Add(new MovieConfiguration());
            modelBuilder.Configurations.Add(new RentalConfiguration());            
        }
    }
}
