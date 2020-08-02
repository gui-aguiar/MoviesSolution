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
    /// Implementation of the IRepository<T> to store Rentals in a Database;
    /// This class implementis its methods using Dapper and Entity Framework to perform the databae access.
    /// </summary>
    public class RentalRepository : IRepository<Rental>
    {
        #region Fields
        private readonly MoviesDBContext _context;
        #endregion

        #region Constructors
        public RentalRepository(MoviesDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Public metods

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
            // EF implementation
            /*   var repoRental = _context.Rental.SingleOrDefault(r => r.Id == id);
               if (repoRental != null)
                   _context.Rental.Remove(repoRental);            
            */

            // Dapper implementation
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
        #endregion
    }
}
