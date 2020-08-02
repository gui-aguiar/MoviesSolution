using Movies.Database;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;

namespace Movies.Repositories
{
    /// <summary>
    /// Implementation of the IRepository<T> to store Genders in a Database;
    /// This class implementis its methods using Dapper and Entity Framework to perform the databae access.
    /// </summary>
    public class GenderRepository : IRepository<Gender>
    {
        #region Fields
        private readonly MoviesDBContext _context;
        #endregion

        #region Constructors
        public GenderRepository(MoviesDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Public methods

        public IEnumerable<Gender> List()
        {
            // EF implementation
            //return _context.Gender;
            
            // Dapper implementation
            var connectionString = ConfigurationManager.ConnectionStrings["MoviesDB"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Gender>
                ("Select * From Movies.Genders").ToList();
            }
        }

        public Gender Get(int id)
        {
            // EF implementation
            return _context.Gender.SingleOrDefault(g => g.Id == id);

            // Dapper implementation
            /*var connectionString = ConfigurationManager.ConnectionStrings["MoviesDB"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QuerySingleOrDefault<Gender>
                ("Select * From Movies.Genders WHERE Id = @Id", new { id });
            }*/
        }

        public void Add(Gender item)
        {
            _context.Gender.Add(item);
        }

        public void Update(int id, Gender item)
        {
            var repoGender = _context.Gender.SingleOrDefault(g => g.Id == id);
            if (repoGender != null)
            {
                repoGender.CreationDateTime = item.CreationDateTime;
                repoGender.Enabled = item.Enabled;
                repoGender.Name = item.Name;
            }
        }

        public void Delete(int id)
        {
            // EF implementation
            /*var repoGender = _context.Gender.SingleOrDefault(g => g.Id == id);
            if (repoGender != null)
                _context.Gender.Remove(repoGender);*/

            // Dapper implementation
            var connectionString = ConfigurationManager.ConnectionStrings["MoviesDB"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var gender = connection.QuerySingleOrDefault<Gender>
                ("Select * From Movies.Genders WHERE Id = @Id", new { id });

                if (gender != null)
                {
                    connection.Execute("DELETE From Movies.Genders WHERE Id = @Id", new { id });
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
