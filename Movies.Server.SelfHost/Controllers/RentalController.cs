using Movies.Business;
using Movies.Models;
using Movies.Server.SelfHost.Common;
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
        #region Fields
        private readonly RentalBusiness _rentalBusiness;
        private static readonly JsonMediaTypeFormatter fJsonMTF = new JsonMediaTypeFormatter();
        #endregion

        #region Contructors
        public RentalController()
        {
            _rentalBusiness = new RentalBusiness();
        }
        #endregion

        #region Public methods

        /// <summary>
        /// List all Rentals
        /// </summary>
        /// <returns>Json containing all Rentals and its properties. 
        /// Includes its Movies info with theirs Gender info. 
        /// </returns>
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

        /// <summary>
        /// Retrieves specific Rental identified by its Id
        /// </summary>
        /// <param name="id">The Rental Id specified by an integer</param>
        /// <returns>Json containing the Rental and its Movies with theirs info </returns>
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

        /// <summary>
        /// Creates and stores a Rental
        /// </summary>
        /// <param name="rental">The Rental properties as JSon.
        /// To create a Rental and relate it to a list of Movie, the Movies Ids must be provided.
        /// In case of provide one invalid Movie Id, a error message will be returned. 
        /// This method do not crate new Movies or Genders.
        /// </param>
        /// <returns>Json containing the created Rentals and its Movies with theirs info</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Add(Rental rental)
        {
            HttpResponseMessage response = null;
            try
            {
                if (!CPFUtils.ValidateFormat(rental.CustomerCPF))
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.ReasonPhrase = Consts.VALIDATION_ERROR_RESPONSE_PHRASE;
                    response.Content = new StringContent(Consts.C_RENTAL_CPF_ERROR_MESSAGE);
                }                
                else if (!_rentalBusiness.ValidateRentalRelations(rental))
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_MOVIE_NOT_FOUND;
                    response.Content = new StringContent(Consts.C_RENTAL_INVALID_MOVIE_ERROR_MESSAGE);
                }
                else
                {
                    IEnumerable<int> moviesIds = rental.MoviesList.Select(m => m.Id);
                    _rentalBusiness.FillRentalMovies(rental, moviesIds);
                    
                    _rentalBusiness.Add(rental);
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

        /// <summary>
        /// Updates and store a Rental
        /// </summary>
        /// <param name="id">The Id of the Rental that will be updated</param>
        /// <param name="rental">The new Rental properties as JSon. 
        /// To edit the Movies list related to the Rental, the Movies Ids must be provided.
        /// In case of provide one invalid Movie Id, an error message will be returned. 
        /// This method do not crate or edit the Movies and theirs Genders properties.
        /// </param>
        /// <returns>Json containing the Rental info after the update</returns>
        [HttpPut]
        public async Task<HttpResponseMessage> Update(int id, Rental rental)
        {
            HttpResponseMessage response = null;
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
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.ReasonPhrase = Consts.VALIDATION_ERROR_RESPONSE_PHRASE;
                    response.Content = new StringContent(Consts.C_RENTAL_CPF_ERROR_MESSAGE);
                }
                else if (!_rentalBusiness.ValidateRentalRelations(rental))
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_MOVIE_NOT_FOUND;
                    response.Content = new StringContent(Consts.C_RENTAL_INVALID_MOVIE_ERROR_MESSAGE);
                }
                else 
                {
                    IEnumerable<int> moviesIds = rental.MoviesList.Select(m => m.Id);
                    _rentalBusiness.FillRentalMovies(rental, moviesIds);
                        
                    _rentalBusiness.Update(id, rental);
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
                else
                {
                    _rentalBusiness.Delete(id);
                    await _rentalBusiness.ApplyChagesAsync();

                    response = Request.CreateResponse();
                    response.Content = new StringContent(Consts.C_RENTAL_DELETED);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.ReasonPhrase = ExceptionUtils.GetErrorMessages(ex);
            }
            return response;
        }
        #endregion        
    }
}
