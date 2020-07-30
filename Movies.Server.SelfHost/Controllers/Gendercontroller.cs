using Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using Movies.Server.SelfHost.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Movies.Server.SelfHost.Controllers
{
    public class Gendercontroller : ApiController
    {
        private readonly IRepository<Gender> _genderBusiness;
        public Gendercontroller()
        {
            _genderBusiness = AutofacConfigurator.Instance.Container.Resolve<IRepository<Gender>>();
        }

        public IEnumerable<Gender> Get()
        {
            return new List<Gender>();
        }
        public Gender Get(int id)
        {
            return new Gender();//planetas.Where(p => p.Id == id).FirstOrDefault();
        }

        public Gender Add(Gender gender)
        {
            return new Gender();//planetas.Where(p => p.Id == id).FirstOrDefault();

        }

        public void Remove(int id)
        {
            // products.RemoveAll(p => p.Id == id); 
        }
    }
}
