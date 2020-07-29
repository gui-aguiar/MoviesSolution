using System.Collections.Generic;

namespace Movies.Interfaces.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> List();
        void Add(T item);
        T Get(int id);
        void Update(T temperature);
        void Delete(int pId);
    }
}
