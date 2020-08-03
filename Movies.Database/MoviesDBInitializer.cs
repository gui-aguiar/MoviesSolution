using System.Data.Entity;

namespace Movies.Database
{    
    /// <summary>
    /// Entity Framework Class that defines the initiliaztion type of the database.
    /// This class can be used to seed data into the database
    /// </summary>
    public class MoviesDBInitializer : CreateDatabaseIfNotExists<MoviesDBContext>
    {
        protected override void Seed(MoviesDBContext context)
        {
            base.Seed(context);
        }
    }
}
