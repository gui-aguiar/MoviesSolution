using Movies.Database;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Business
{
    public class RentalBusiness : IRepository<Rental>
    {
        private readonly MoviesDBContext _context;
        public RentalBusiness(MoviesDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Rental> List()
        {
            return _context.Rental
                    .Include(r => r.MoviesList);
        }

        public Rental Get(int id)
        {
            return _context.Rental
                   .Include(r => r.MoviesList)
                   .SingleOrDefault(r => r.Id == id);
        }
        public void AddAsync(Rental item)
        {
            _context.Rental.Add(item);            
        }
        public void UpdateAsync(int id, Rental item)
        {
            var repoRental = _context.Rental.SingleOrDefault(r => r.Id == id);
            if (repoRental != null)
            {
                repoRental.RentalDateTime = item.RentalDateTime;
                repoRental.CustomerCPF = item.CustomerCPF;
                repoRental.MoviesList = item.MoviesList;
            }
        }
        public void DeleteAsync(int id)
        {
            var repoRental = _context.Rental.SingleOrDefault(r => r.Id == id);
            if (repoRental != null)
                _context.Rental.Remove(repoRental);            
        }
        public async Task ApplyChagesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
