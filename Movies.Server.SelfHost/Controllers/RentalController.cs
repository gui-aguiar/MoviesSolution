using Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using Movies.Server.SelfHost.Configuration;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;

namespace Movies.Server.SelfHost.Controllers
{
    public class RentalController : ApiController
    {
        private readonly IRepository<Rental> _rentalBusiness;
        public RentalController()
        {
            _rentalBusiness = AutofacConfigurator.Instance.Container.Resolve<IRepository<Rental>>();
        }
        public IEnumerable<Rental> Get()
        {
            return _rentalBusiness.List();
        }
        public Rental Get(int id)
        {
            return _rentalBusiness.Get(id);
        }
        public async Task AddAsync(Rental rental)
        {
            _rentalBusiness.AddAsync(rental);
            await _rentalBusiness.ApplyChagesAsync();
        }
        public async Task Update(Rental rental)
        {
            _rentalBusiness.UpdateAsync(rental);
            await _rentalBusiness.ApplyChagesAsync();
        }
        public async Task Remove(int id)
        {
            _rentalBusiness.DeleteAsync(id);
            await _rentalBusiness.ApplyChagesAsync();
        }                
    }
}
