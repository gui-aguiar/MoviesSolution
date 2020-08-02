using Autofac;
using Movies.Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business
{
    public class GenderBusiness
    {
        private readonly IRepository<Gender> _repository;
        public GenderBusiness()
        {
            _repository = AutofacConfigurator.Instance.Container.Resolve<IRepository<Gender>>();
        }

        public GenderBusiness(IRepository<Gender> repository)
        {
            _repository = repository;
        }

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
    }
}
