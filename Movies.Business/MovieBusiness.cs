using Movies.Database;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Business
{
    public class MovieBusiness : IRepository<Movie>
    {
        private readonly MoviesDBContext _context;
        public MovieBusiness(MoviesDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Movie> List()
        {
            return _context.Movie;
        }

        public Movie Get(int id)
        {
            return _context.Movie.SingleOrDefault(m => m.Id == id);
        }

        public void AddAsync(Movie item)
        {
            _context.Movie.Add(item);            
        }

        public void UpdateAsync(Movie item)
        {
            var repoMovie = _context.Movie.SingleOrDefault(m => m.Id == item.Id);
            if (repoMovie != null)
            {
                repoMovie.Name = item.Name;
                repoMovie.Enabled = item.Enabled;
                repoMovie.Gender = item.Gender;                
            }
        }

        public void DeleteAsync(int id)
        {
            var repoMovie = _context.Movie.SingleOrDefault(m => m.Id == id);
            if (repoMovie != null)
                _context.Movie.Remove(repoMovie);
        }

        public async Task ApplyChagesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
