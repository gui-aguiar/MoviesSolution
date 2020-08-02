using Autofac;
using Movies.Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business
{
    public class RentalBusiness
    {
        private readonly IRepository<Rental> _repository;
        public RentalBusiness()
        {
            _repository = AutofacConfigurator.Instance.Container.Resolve<IRepository<Rental>>();
        }

        public RentalBusiness(IRepository<Rental> repository)
        {
            _repository = repository;
        }

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
    }
}
