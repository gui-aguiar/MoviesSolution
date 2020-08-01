using Movies.Database;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
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
            return _context.Rental;
        }

        public Rental Get(int id)
        {
            return _context.Rental.SingleOrDefault(r => r.Id == id);
        }
        public void AddAsync(Rental item)
        {
            _context.Rental.Add(item);            
        }
        public void UpdateAsync(Rental item)
        {
            var repoRental = _context.Rental.SingleOrDefault(r => r.Id == item.Id);
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
