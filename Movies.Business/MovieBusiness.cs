using Autofac;
using Movies.Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business
{
    public class MovieBusiness
    {
        private readonly IRepository<Movie> _repository;
        public MovieBusiness()
        {
            _repository = AutofacConfigurator.Instance.Container.Resolve<IRepository<Movie>>();
        }

        public MovieBusiness(IRepository<Movie> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Movie> List()
        {
            return _repository.List();
        }

        public Movie Get(int id)
        {
            return _repository.Get(id);
        }

        public void Add(Movie item)
        {
            _repository.Add(item);
        }

        public void Update(int id, Movie item)
        {
            var repoMovie = _repository.Get(id);
            if (repoMovie != null)
            {
                _repository.Update(id, item);
            }
        }

        public void Delete(int id)
        {
            var repoMovie = _repository.Get(id);
            if (repoMovie != null)
            {
                _repository.Delete(id);
            }
        }
        public async Task ApplyChagesAsync()
        {
            await _repository.ApplyChagesAsync();
        }
    }
}
