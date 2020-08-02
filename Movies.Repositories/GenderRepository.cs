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
    public class GenderRepository : IRepository<Gender>
    {
        private readonly MoviesDBContext _context;
        public GenderRepository(MoviesDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Gender> List()
        {
            //return _context.Gender;
            var connectionString = ConfigurationManager.ConnectionStrings["MoviesDB"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Gender>
                ("Select * From Movies.Genders").ToList();
            }
        }

        public Gender Get(int id)
        {
            return _context.Gender.SingleOrDefault(g => g.Id == id);

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
            /*var repoGender = _context.Gender.SingleOrDefault(g => g.Id == id);
            if (repoGender != null)
                _context.Gender.Remove(repoGender);*/

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
    }
}
