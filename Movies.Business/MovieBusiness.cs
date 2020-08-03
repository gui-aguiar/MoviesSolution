using Autofac;
using Movies.Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business
{
    /// <summary>
    /// Class responsible to implements the Movie business logic. It has a Movie repository used to perform its operations. 
    /// Its initial implementation is simple but it is already prepared for further rules and complexity 
    /// </summary>
    public class MovieBusiness
    {
        #region Fields
        private readonly IRepository<Movie> _repository;
        #endregion

        #region Constructors
        
        /// <summary>
        /// Main constructor, used by the release application. It retrieves the filed repository from the Autofac dependecy injection container. 
        /// </summary>
        public MovieBusiness()
        {
            _repository = AutofacConfigurator.Instance.Container.Resolve<IRepository<Movie>>();
        }

        /// <summary>
        /// Constructor created to be used in unit tests. The constructor provides the repository to be used by the business level.
        /// This way, it is possible to provide a mock repository and develop ideal unit tests
        /// <param name="repository">A Movie repository</param>        
        public MovieBusiness(IRepository<Movie> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
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
        #endregion
    }
}
