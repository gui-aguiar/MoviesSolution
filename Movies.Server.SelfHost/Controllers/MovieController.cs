using Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using Movies.Server.SelfHost.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Movies.Server.SelfHost.Controllers
{
    public class MovieController : ApiController
    {
        private readonly IRepository<Movie> _movieBusiness;
        public MovieController()
        {
            _movieBusiness = AutofacConfigurator.Instance.Container.Resolve<IRepository<Movie>>();
        }

        public IEnumerable<Movie> Get()
        {
            return _movieBusiness.List();
        }
        public Movie Get(int id)
        {
            return _movieBusiness.Get(id);
        }

        public async Task Add(Movie movie)
        {
            _movieBusiness.AddAsync(movie);
            await _movieBusiness.ApplyChagesAsync();
        }
        public async Task Update(Movie movie)
        {
            _movieBusiness.UpdateAsync(movie);
            await _movieBusiness.ApplyChagesAsync();
        }
        public async Task Remove(int id)
        {
            _movieBusiness.DeleteAsync(id);
            await _movieBusiness.ApplyChagesAsync();
        }
    }
}
