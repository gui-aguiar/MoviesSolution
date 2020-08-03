using Autofac;
using Movies.Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
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
        #endregion

        #region Constructors

        /// <summary>
        /// Main constructor, used by the release application. It retrieves the filed repository from the Autofac dependecy injection container. 
        /// </summary>
        public RentalBusiness()
        {
            _repository = AutofacConfigurator.Instance.Container.Resolve<IRepository<Rental>>();
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

        #endregion
    }
}
