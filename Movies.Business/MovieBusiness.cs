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
        private readonly GenderBusiness _genderBusiness;
        private readonly IRepository<Movie> _repository;
        #endregion

        #region Constructors
        
        /// <summary>
        /// Main constructor, used by the release application. It retrieves the filed repository from the Autofac dependecy injection container. 
        /// </summary>
        public MovieBusiness()
        {
            _repository = AutofacConfigurator.Instance.Container.Resolve<IRepository<Movie>>();
            _genderBusiness = new GenderBusiness();
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


        /// <summary>
        /// Check if the Movie object has the right relations with the Gender object. All Movie objects must have a valid database Gender object
        /// </summary>
        /// <param name="movie">The Movie object which will have its Gender relation verified</param>
        /// <returns>Boolean value representing whether the Movie object is consistent or not</returns>
        public bool ValidateMovieRelations(Movie movie)
        {
            if (movie.Gender == null)
              return false;

            var gender = _genderBusiness.Get(movie.Gender.Id);
            return gender != null;            
        }

        /// <summary>
        /// Fill the Movie object with the database Gender got by the provided id.
        /// </summary>
        /// <param name="movie">The Movie object which will have its Gender filled</param
        /// <param name="genderId">The id of the Gender object that will be used to fill the Movie</param
        public void FillGender(Movie movie, int genderId)
        {
            var gender = _genderBusiness.Get(genderId);
            if(gender != null)
            {
                movie.Gender = gender;
            }            
        }

        public async Task ApplyChagesAsync()
        {
            await _repository.ApplyChagesAsync();
        }

        #endregion
    }
}
