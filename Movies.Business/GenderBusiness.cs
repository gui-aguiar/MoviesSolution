using Movies.Database;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Business
{
    public class GenderBusiness : IRepository<Gender>
    {
        private readonly MoviesDBContext _context;
        public GenderBusiness(MoviesDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Gender> List()
        {
            return _context.Gender;
        }

        public Gender Get(int id)
        { 
            return _context.Gender.SingleOrDefault(g => g.Id == id);
        }

        public void AddAsync(Gender item)
        {
            _context.Gender.Add(item);            
        }
     
        public void UpdateAsync(int id, Gender item)
        {
            var repoGender = _context.Gender.SingleOrDefault(g => g.Id == id);
            if (repoGender != null)
            {
                repoGender.CreationDateTime = item.CreationDateTime;
                repoGender.Enabled = item.Enabled;
                repoGender.Name = item.Name;                
            }
        }

        public void DeleteAsync(int id)
        {
            var repoGender = _context.Gender.SingleOrDefault(g => g.Id == id);
            if (repoGender != null)
                _context.Gender.Remove(repoGender);               
        }
        public async Task ApplyChagesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
