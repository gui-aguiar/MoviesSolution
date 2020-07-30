using System.Data.Entity;

namespace Movies.Database
{
    public class MoviesDBInitializer : CreateDatabaseIfNotExists<MoviesDBContext>
    {
        protected override void Seed(MoviesDBContext context)
        {
            base.Seed(context);
        }
    }
}
