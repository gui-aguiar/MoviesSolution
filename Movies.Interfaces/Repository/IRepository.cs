using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Interfaces.Repository
{   
    /// <summary>
    /// Interface representing the main operations of a Generic repository
    /// </summary>
    public interface IRepository<T>
    {
        IEnumerable<T> List();
        T Get(int id);
        void Add(T item);
        void Update(int id, T item);
        void Delete(int id);
        Task ApplyChagesAsync();
    }
}
