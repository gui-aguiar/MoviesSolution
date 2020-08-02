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
    public class MovieRepository : IRepository<Movie>
    {
        private readonly MoviesDBContext _context;
        public MovieRepository(MoviesDBContext context)
        {
            _context = context;
        }
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
            /*_context.Movie.Remove(repoMovie);*/
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
    }
}
