using Movies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Interfaces.Repository
{
    public interface IGenderRepository
    {
        IEnumerable<Gender> List();
        Gender Get(int id);
        Task AddAsync(Gender item);
        Task UpdateAsync(Gender temperature);
        Task DeleteAsync(int id);
    }
}
