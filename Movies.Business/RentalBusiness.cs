using Autofac;
using Movies.Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movies.Business
{
    /// <summary>
    /// Class responsible to implements the Rental business logic. It has a Rental repository used to perform its operations. 
    /// Its initial implementation is simple but it is already prepared for further rules and complexity 
    /// </summary>
    public class RentalBusiness
    {
        #region Fields
        private readonly IRepository<Rental> _repository;
        private readonly MovieBusiness _movieBusiness;
        #endregion

        #region Constructors

        /// <summary>
        /// Main constructor, used by the release application. It retrieves the filed repository from the Autofac dependecy injection container. 
        /// </summary>
        public RentalBusiness()
        {
            _repository = AutofacConfigurator.Instance.Container.Resolve<IRepository<Rental>>();
            _movieBusiness = new MovieBusiness();
        }

        /// <summary>
        /// Constructor created to be used in unit tests. The constructor provides the repository to be used by the business level.
        /// This way, it is possible to provide a mock repository and develop ideal unit tests
        /// <param name="repository">A Rental repository</param>        
        public RentalBusiness(IRepository<Rental> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public IEnumerable<Rental> List()
        {
            return _repository.List();
        }

        public Rental Get(int id)
        {
            return _repository.Get(id);
        }

        public void Add(Rental item)
        {
            _repository.Add(item);
        }

        public void Update(int id, Rental item)
        {
            var repoRental = _repository.Get(id);
            if (repoRental != null)
            {
                _repository.Update(id, item);
            }
        }

        public void Delete(int id)
        {
            var repoRental = _repository.Get(id);
            if (repoRental != null)
            {
                _repository.Delete(id);
            }
        }
        public async Task ApplyChagesAsync()
        {
            await _repository.ApplyChagesAsync();
        }


        /// <summary>
        /// Checkif the Rental object has valid database Movies list. All the movies must have a valid Id.
        /// </summary>
        /// <param name="rental">The Rental object that will have its Movies list verified</param>        
        /// <returns>Boolean value representing whether the Rental object is consistent or not</returns>
        public bool ValidateRentalRelations(Rental rental)
        {
            foreach (Movie m in rental.MoviesList)
            {
                var repoMovie = _movieBusiness.Get(m.Id);
                if (repoMovie == null)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Fill the Rental object with the database Movie retrieved using the provided Ids list.
        /// </summary>
        /// <param name="rental">The Movie object which will have its Movies list filled</param
        /// <param name="moviesIds">An integer enumerable containing all the movies Ids used to get the movies from the database</param
        public void FillRentalMovies(Rental rental, IEnumerable<int> moviesIds)
        {
            var movies = new List<Movie>();
            foreach (int id in moviesIds)
            {
                var repoMovie = _movieBusiness.Get(id);
                if (repoMovie != null)
                {
                    movies.Add(repoMovie);
                }
            }
            rental.MoviesList = movies;
        }
        #endregion
    }

}
