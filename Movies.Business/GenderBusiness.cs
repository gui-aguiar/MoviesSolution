using Movies.Database;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;

namespace Movies.Business
{
    public class GenderBusiness : IRepository<Gender>
    {
        private readonly MoviesDBContext _context;
        public GenderBusiness(MoviesDBContext context)
        {
            _context = context;
        }
        public void Add(Gender item)
        {
           //return _con
        }

        public void Delete(int pId)
        {
            throw new System.NotImplementedException();
        }

        public Gender Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Gender> List()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Gender temperature)
        {
            throw new System.NotImplementedException();
        }
    }
}
