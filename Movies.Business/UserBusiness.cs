using Autofac;
using Movies.Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Business
{
    public class UserBusiness
    {
        /// <summary>
        /// Class responsible to implements the User business logic. It has a User repository used to perform its operations. 
        /// Its initial implementation is simple but it is already prepared for further rules and complexity 
        /// </summary>

        #region Fields
        private readonly IRepository<User> _repository;        
        #endregion
        
        #region Constructors
        /// <summary>
        /// Main constructor, used by the release application. It retrieves the filed repository from the Autofac dependecy injection container. 
        /// </summary>
        public UserBusiness()
        {
            _repository = AutofacConfigurator.Instance.Container.Resolve<IRepository<User>>();            
        }

        /// <summary>
        /// Constructor created to be used in unit tests. The constructor provides the repository to be used by the business level.
        /// This way, it is possible to provide a mock repository and develop ideal unit tests
        /// <param name="repository">A User repository</param>        
        public UserBusiness(IRepository<User> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public IEnumerable<User> List()
        {
            return _repository.List();
        }

        public void Add(User user)
        {
            _repository.Add(user);
        }

        /// <summary>
        /// Confirm an User login checking if theres one user with the same name and password in the database
        /// <param name="login">User login</param>        
        /// <param name="pass">User password</param>
        /// <returns>Boolean value representing the login success</returns>         
        public bool Login(string login, string pass)
        {
            var user = _repository.List().SingleOrDefault<User>(u => u.Login == login);
            if (user == null)
                return false;

            return user.Password == pass;
        }

        /// <summary>
        /// Validates if an User object is well formed
        /// <param name="user">User to be checked</param>        
        /// <returns>Boolean value representing if the User is well formed or not</returns> 
        public bool ValidateUser(User user)
        {
            return (user.Login != String.Empty) && (user.Login != String.Empty);
        }

        public async Task ApplyChagesAsync()
        {
            await _repository.ApplyChagesAsync();
        }

        #endregion

    }
}