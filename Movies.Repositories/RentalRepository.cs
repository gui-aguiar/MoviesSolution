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
    public class RentalRepository : IRepository<Rental>
    {
        private readonly MoviesDBContext _context;
        public RentalRepository(MoviesDBContext context)
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
        public void Add(Rental item)
        {
            _context.Rental.Add(item);
        }
        public void Update(int id, Rental item)
        {
            var repoRental = _context.Rental.SingleOrDefault(r => r.Id == id);
            if (repoRental != null)
            {
                repoRental.RentalDateTime = item.RentalDateTime;
                repoRental.CustomerCPF = item.CustomerCPF;
                repoRental.MoviesList = item.MoviesList;
            }
        }
        public void Delete(int id)
        {
            /*   var repoRental = _context.Rental.SingleOrDefault(r => r.Id == id);
               if (repoRental != null)
                   _context.Rental.Remove(repoRental);            */

            var connectionString = ConfigurationManager.ConnectionStrings["MoviesDB"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var rental = connection.QuerySingleOrDefault<Rental>
                ("Select * From Movies.Rentals WHERE Id = @Id", new { id });

                if (rental != null)
                {
                    connection.Execute("DELETE From Movies.Rentals WHERE Id = @Id", new { id });
                }
            }
        }
        public async Task ApplyChagesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
