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
    public class MovieController : ApiController
    {
        private readonly MovieBusiness _movieBusiness;
        private readonly GenderBusiness _genderBusiness;
        private static readonly JsonMediaTypeFormatter fJsonMTF = new JsonMediaTypeFormatter();
        public MovieController()
        {
            _movieBusiness = new MovieBusiness();
            _genderBusiness = new GenderBusiness();
        }

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
                    response.Content = new StringContent("Create movie gender first");
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
                        // no gender properties will be edited                        
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
                        response.Content = new StringContent("Create movie gender first");
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
    }
}
