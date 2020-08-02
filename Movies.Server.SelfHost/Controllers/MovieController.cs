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
    /// <summary>
    /// Movie controller Class. Exposes the Movie related APIs.
    /// </summary>
    public class MovieController : ApiController
    {
        #region Fields
        
        private readonly MovieBusiness _movieBusiness;
        private readonly GenderBusiness _genderBusiness;
        private static readonly JsonMediaTypeFormatter fJsonMTF = new JsonMediaTypeFormatter();

        #endregion

        #region Contructors
        public MovieController()
        {
            _movieBusiness = new MovieBusiness();
            _genderBusiness = new GenderBusiness();
        }
        #endregion

        #region Public methods

        /// <summary>
        /// List all Movies
        /// </summary>
        /// <returns>Json containing all Movies and its properties. Includes its Gender info. </returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                var movies = _movieBusiness.List();
                if (!movies.Any())
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_GENDER_NOT_FOUND;
                }
                else
                {
                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<IEnumerable<Movie>>(movies, fJsonMTF, Consts.C_MT_JSON);
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
        /// Retrieves specific Movie identified by its Id
        /// </summary>
        /// <param name="id">The Movie Id specified by an integer</param>
        /// <returns>Json containing the Movie and its Gender info </returns>
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            try
            {
                var movie = _movieBusiness.Get(id);
                if (movie == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_MOVIE_NOT_FOUND;
                }
                else
                {
                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<Movie>(movie, fJsonMTF, Consts.C_MT_JSON);
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
        /// Creates and store a Movie
        /// </summary>
        /// <param name="movie">The Movie properties as JSon.
        /// To create a Movie related to a Gender, the Gender Id must be provided.
        /// In case of provide an invalid Gender Id , a error message will be returned. 
        /// This method do not crate new Genders.
        /// </param>
        /// <returns>Json containing the created Moavie and its Gender info </returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Add(Movie movie)
        {
            HttpResponseMessage response;
            try
            {
                var gender = _genderBusiness.Get(movie.Gender.Id);
                if (gender != null)
                {
                    movie.Gender = gender;
                    _movieBusiness.Add(movie);
                    await _movieBusiness.ApplyChagesAsync();

                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<Movie>(movie, fJsonMTF, Consts.C_MT_JSON);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_GENDER_NOT_FOUND;
                    response.Content = new StringContent($"Gender with id {movie.Gender.Id} does not existis. Crate this Gender first");
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
        /// Updates and store a Movie
        /// </summary>
        /// <param name="id">The Id of the Movie that will be updated</param>
        /// <param name="movie">The new Movie properties as JSonThe Movie properties as JSon.
        /// To edit the Gender related to the Movie, the Gender Id must be provided.
        /// In case of provide an invalid Gender Id, an error message will be returned. 
        /// This method do not crate or edit the Gender properties.
        /// </param>
        /// <returns>Json containing the Movie info after the update</returns>
        [HttpPut]
        public async Task<HttpResponseMessage> Update(int id,Movie movie)
        {
            HttpResponseMessage response;
            try
            {
                var repoMovie = _movieBusiness.Get(id);
                if (repoMovie == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_MOVIE_NOT_FOUND;
                }
                else
                {
                    var gender = _genderBusiness.Get(movie.Gender.Id);
                    if (gender != null)
                    {                          
                        movie.Gender = gender;
                        _movieBusiness.Update(id, movie);
                        await _movieBusiness.ApplyChagesAsync();

                        response = Request.CreateResponse();
                        response.Content = new ObjectContent<Movie>(repoMovie, fJsonMTF, Consts.C_MT_JSON);
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.NotFound);
                        response.ReasonPhrase = Consts.C_GENDER_NOT_FOUND;
                        response.Content = new StringContent($"Gender with id {movie.Gender.Id} does not existis. Crate this Gender first");
                    }
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
        /// Deletes a Movie specified by its Id
        /// </summary>
        /// <param name="id">The Id of the Movie that will be deleted</param>
        /// <returns>String message with the deletion status</returns>
        [HttpDelete]
        public async Task<HttpResponseMessage> Remove(int id)
        {
            HttpResponseMessage response;
            try
            {
                var movie = _movieBusiness.Get(id);
                if (movie == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_MOVIE_NOT_FOUND;
                } 
                else
                {
                    _movieBusiness.Delete(id);
                    await _movieBusiness.ApplyChagesAsync();

                    response = Request.CreateResponse();
                    response.Content = new StringContent(Consts.C_MOVIE_DELETED);
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
