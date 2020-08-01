using Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using Movies.Server.SelfHost.Common;
using Movies.Server.SelfHost.Configuration;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using Utils;

namespace Movies.Server.SelfHost.Controllers
{
    public class RentalController : ApiController
    {
        private readonly IRepository<Rental> _rentalBusiness;
        private readonly IRepository<Movie> _movieBusiness;
        private static readonly JsonMediaTypeFormatter fJsonMTF = new JsonMediaTypeFormatter();
        public RentalController()
        {
            _rentalBusiness = AutofacConfigurator.Instance.Container.Resolve<IRepository<Rental>>();
            _movieBusiness = AutofacConfigurator.Instance.Container.Resolve<IRepository<Movie>>();
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                var rentals = _rentalBusiness.List();
                if (!rentals.Any())
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_GENDER_NOT_FOUND;
                }
                else
                {
                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<IEnumerable<Rental>>(rentals, fJsonMTF, Consts.C_MT_JSON);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.ReasonPhrase = ExceptionUtils.GetErrorMessages(ex);
            }
            return response;
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            try
            {
                var rental = _rentalBusiness.Get(id);
                if (rental == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_RENTAL_NOT_FOUND;
                }
                else
                {
                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<Rental>(rental, fJsonMTF, Consts.C_MT_JSON);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.ReasonPhrase = ExceptionUtils.GetErrorMessages(ex);
            }
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Add(Rental rental)
        {
            HttpResponseMessage response;
            try
            {
                if (!CPFUtils.ValidateFormat(rental.CustomerCPF))
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);  // 422 UNPROCESSABLE ENTITY
                    response.ReasonPhrase = "Validation error";
                    response.Content = new StringContent("CPF field is not correctly formatted.");
                }                
                else
                {
                    var moviesSent = new List<Movie>();
                    if (rental.MoviesList.Count > 0) {                        
                        foreach (Movie m in rental.MoviesList)
                        {
                            var repoMovie = _movieBusiness.Get(m.Id);
                            if (repoMovie != null)
                            {
                                moviesSent.Add(repoMovie);
                            }
                            else
                            {
                                response = Request.CreateResponse(HttpStatusCode.NotFound);
                                response.ReasonPhrase = Consts.C_MOVIE_NOT_FOUND;
                                response.Content = new StringContent($"Movie with id {m.Id} does not existis. Crate movie first");
                                return response;
                            }
                        }
                    }

                    rental.MoviesList = moviesSent;
                    _rentalBusiness.AddAsync(rental);
                    await _rentalBusiness.ApplyChagesAsync();

                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<Rental>(rental, fJsonMTF, Consts.C_MT_JSON);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.ReasonPhrase = ExceptionUtils.GetErrorMessages(ex);
            }
            return response;
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(int id, Rental rental)
        {
            HttpResponseMessage response;
            try
            {
                var repoRental = _rentalBusiness.Get(id);
                if (repoRental == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_MOVIE_NOT_FOUND;
                }
                else if(!CPFUtils.ValidateFormat(rental.CustomerCPF))
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);  // 422 UNPROCESSABLE ENTITY
                    response.ReasonPhrase = "Validation error";
                    response.Content = new StringContent("CPF field is not correctly formatted.");
                }
                else
                {
                    var moviesSent = new List<Movie>();
                    if (rental.MoviesList.Count > 0)
                    {
                        foreach (Movie m in rental.MoviesList)
                        {
                            var repoMovie = _movieBusiness.Get(m.Id);
                            if (repoMovie != null)
                            {
                                moviesSent.Add(repoMovie);
                            }
                            else
                            {
                                response = Request.CreateResponse(HttpStatusCode.NotFound);
                                response.ReasonPhrase = Consts.C_MOVIE_NOT_FOUND;
                                response.Content = new StringContent("Movie with id {m.id} does not existis. Crate movie first");
                                return response;
                            }
                        }
                    }
                    
                    rental.MoviesList = moviesSent;
                    _rentalBusiness.UpdateAsync(id, rental);
                    await _rentalBusiness.ApplyChagesAsync();

                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<Rental>(repoRental, fJsonMTF, Consts.C_MT_JSON);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.ReasonPhrase = ExceptionUtils.GetErrorMessages(ex);
            }
            return response;
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Remove(int id)
        {
            HttpResponseMessage response;
            try
            {
                var rental = _rentalBusiness.Get(id);
                if (rental == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_RENTAL_NOT_FOUND;
                }

                _rentalBusiness.DeleteAsync(id);
                await _rentalBusiness.ApplyChagesAsync();

                response = Request.CreateResponse();
                response.Content = new StringContent(Consts.C_RENTAL_DELETED);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.ReasonPhrase = ExceptionUtils.GetErrorMessages(ex);
            }
            return response;
        }                
    }
}
