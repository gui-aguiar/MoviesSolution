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
    public class Gendercontroller : ApiController
    {
        private readonly GenderBusiness _genderBusiness;
        private static readonly JsonMediaTypeFormatter fJsonMTF = new JsonMediaTypeFormatter();
        public Gendercontroller()
        {
            _genderBusiness = new GenderBusiness();
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                var genders = _genderBusiness.List();
                if(!genders.Any())
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_MOVIE_NOT_FOUND;
                }
                else
                {                    
                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<IEnumerable<Gender>>(genders, fJsonMTF, Consts.C_MT_JSON);
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
                var gender = _genderBusiness.Get(id);
                if (gender == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_GENDER_NOT_FOUND; 
                } 
                else 
                {                    
                    response = Request.CreateResponse();                 
                    response.Content = new ObjectContent<Gender>(gender, fJsonMTF, Consts.C_MT_JSON);
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
        public async Task<HttpResponseMessage> AddAsync(Gender gender)
        {
            HttpResponseMessage response;
            try
            {
                _genderBusiness.Add(gender);
                await _genderBusiness.ApplyChagesAsync();

                response = Request.CreateResponse();
                response.Content = new ObjectContent<Gender>(gender, fJsonMTF, Consts.C_MT_JSON);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.ReasonPhrase = ExceptionUtils.GetErrorMessages(ex);
            }
            return response;            
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(int id, Gender gender)
        {
            HttpResponseMessage response;
            try
            {
                var repoGender = _genderBusiness.Get(id);
                if (repoGender == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_GENDER_NOT_FOUND;
                }
                else
                {                    
                    _genderBusiness.Update(id ,gender);
                    await _genderBusiness.ApplyChagesAsync();

                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<Gender>(repoGender, fJsonMTF, Consts.C_MT_JSON);
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
                var gender = _genderBusiness.Get(id);
                if (gender == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_GENDER_NOT_FOUND;
                }
                else
                {
                    _genderBusiness.Delete(id);
                    await _genderBusiness.ApplyChagesAsync();

                    response = Request.CreateResponse();
                    response.Content = new StringContent(Consts.C_GENDER_DELETED);
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
