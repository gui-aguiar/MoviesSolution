using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Interfaces.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> List();
        T Get(int id);
        void AddAsync(T item);
        void UpdateAsync(T item);
        void DeleteAsync(int id);
        Task ApplyChagesAsync();
    }
}
