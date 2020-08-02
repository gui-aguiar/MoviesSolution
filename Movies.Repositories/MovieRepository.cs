using Dapper;
using Movies.Database;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    /// <summary>
    /// Implementation of the IRepository<T> to store Movies in a Database;
    /// This class implementis its methods using Dapper and Entity Framework to perform the databae access.
    /// </summary>
    public class MovieRepository : IRepository<Movie>
    {
        #region Fields
        private readonly MoviesDBContext _context;
        #endregion

        #region Constructors
        public MovieRepository(MoviesDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Public methods
        public IEnumerable<Movie> List()
        {
            return _context.Movie
                  .Include(m => m.Gender);
        }

        public Movie Get(int id)
        {
            return _context.Movie
                    .Include(m => m.Gender)
                    .SingleOrDefault(m => m.Id == id);
        }

        public void Add(Movie item)
        {
            _context.Movie.Add(item);
        }

        public void Update(int id, Movie item)
        {
            var repoMovie = _context.Movie.SingleOrDefault(m => m.Id == id);
            repoMovie.Name = item.Name;
            repoMovie.Enabled = item.Enabled;
            repoMovie.Gender = item.Gender;            
        }

        public void Delete(int id)
        {
            // EF implementation
            /*_context.Movie.Remove(repoMovie);*/

            // Dapper implementation
            var connectionString = ConfigurationManager.ConnectionStrings["MoviesDB"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("DELETE From Movies.Movies WHERE Id = @Id", new { id });
            }
        }

        public async Task ApplyChagesAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
