using Dapper;
using Movies.Database;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    /// <summary>
    /// Implementation of the IRepository<T> to store Users in a Database;
    /// This class implementis its methods using Dapper and Entity Framework to perform the databae access.
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        #region Fields
        private readonly MoviesDBContext _context;
        #endregion

        #region Constructors
        public UserRepository(MoviesDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Public methods
        public IEnumerable<User> List()
        {
            // EF implementation
            return _context.User;

            // Dapper implementation
            /*var connectionString = ConfigurationManager.ConnectionStrings["MoviesDB-Auth"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<User>
                ("Select * From Movies.Users").ToList();
            }*/
        }
        
        public User Get(int id)
        {
            // EF implementation
            return _context.User.SingleOrDefault(u => u.Id == id);

            // Dapper implementation
            /*var connectionString = ConfigurationManager.ConnectionStrings["MoviesDB-Auth"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QuerySingleOrDefault<User>
                ("Select * From Movies.Users WHERE Id = @Id", new { id });
            }*/
        }

        public void Add(User item)
        {
            _context.User.Add(item);
        }

        public void Update(int id, User item)
        {
            var repoUser = _context.User.SingleOrDefault(u => u.Id == id);
            if (repoUser != null)
            {
                repoUser.Login = item.Login;
                repoUser.Password = item.Password;                
            }
        }

        public void Delete(int id)
        {
            // EF implementation
            var repoUser = _context.User.SingleOrDefault(g => g.Id == id);
            if (repoUser != null)
                _context.User.Remove(repoUser);

            // Dapper implementation
            /*var connectionString = ConfigurationManager.ConnectionStrings["MoviesDB"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var user = connection.QuerySingleOrDefault<User>
                ("Select * From Movies.Users WHERE Id = @Id", new { id });

                if (user != null)
                {
                    connection.Execute("DELETE From Movies.Users WHERE Id = @Id", new { id });
                }
            }*/
        }

        public async Task ApplyChagesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
