using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Business.Tests
{
    /// <summary>
    /// Implementation of the IRepository<T> to store Genders in a List;
    /// This class implementis represents the behaviour of a database and is used  to make the tests isolated from a database server
    /// </summary>
    public class GenderListRepository : IRepository<Gender>
    {
        #region Fields
        private readonly List<Gender> _genderList;
        private static int _index = 0;
        #endregion

        #region Contructors
        public GenderListRepository() {
            _genderList  = new List<Gender>();
        }
        #endregion

        #region Public methods

        public IEnumerable<Gender> List()
        {
            return _genderList;
        }

        public Gender Get(int id)
        {
            return _genderList.SingleOrDefault(t => t.Id == id);
        }
    
        public void Add(Gender item)
        {
            item.Id = _index;
            _genderList.Add(item);
            _index++;
        }

        public void Update(int id, Gender item)
        {
            Gender gender = _genderList.SingleOrDefault(t => t.Id == id);
            if (gender != null)
            {   
                gender.CreationDateTime = item.CreationDateTime;
                gender.Enabled = item.Enabled;
                gender.Name = item.Name;
            }
        }

        public void Delete(int id)
        {
            Gender vItem = Get(id);
            if (vItem != null)
            {
                _genderList.Remove(vItem);
            }
        }

        public Task ApplyChagesAsync()
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
