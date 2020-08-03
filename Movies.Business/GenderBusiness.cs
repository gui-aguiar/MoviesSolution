using Autofac;
using Movies.Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business
{
    /// <summary>
    /// Class responsible to implements the Gender business logic. It has a Gender repository used to 
    /// perform its operations. 
    /// Its initial implementation is simple but it is already prepared for further rules and complexity 
    /// </summary>
    public class GenderBusiness
    {
        #region Fields
        private readonly IRepository<Gender> _repository;
        #endregion

        #region Constructors

        /// <summary>
        /// Main constructor, used by the release application. It retrieves the filed repository from
        /// the Autofac dependecy injection container. 
        /// </summary>
        public GenderBusiness()
        {
            _repository = AutofacConfigurator.Instance.Container.Resolve<IRepository<Gender>>();
        }

        /// <summary>
        /// Constructor created to be used in unit tests. The constructor provides the repository to be used by the business level.
        /// This way, it is possible to provide a mock repository and develop ideal unit tests
        /// <param name="repository">A Gender repository</param>        
        public GenderBusiness(IRepository<Gender> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public IEnumerable<Gender> List()
        {
            return _repository.List();           
        }

        public Gender Get(int id)
        { 
            return _repository.Get(id);
        }

        public void Add(Gender item)
        {
            _repository.Add(item);            
        }
     
        public void Update(int id, Gender item)
        {
            var repoGender = _repository.Get(id);
            if (repoGender != null)
            {
                _repository.Update(id, item);
            }
        }

        public void Delete(int id)
        {
            var repoGender = _repository.Get(id);
            if (repoGender != null)
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
