using Autofac;
using Movies.Interfaces.Repository;
using Movies.Models;
using Movies.Server.SelfHost.Configuration;
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
        private readonly IRepository<Gender> _genderBusiness;
        private static readonly JsonMediaTypeFormatter fJsonMTF = new JsonMediaTypeFormatter();
        public static readonly string C_MT_JSON = "application/json";
        public Gendercontroller()
        {   
            _genderBusiness = AutofacConfigurator.Instance.Container.Resolve<IRepository<Gender>>();
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
                    response.ReasonPhrase = "No genders found";
                }
                else
                {                    
                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<IEnumerable<Gender>>(genders, fJsonMTF, C_MT_JSON);
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
                    response.ReasonPhrase = "No gender found";
                } 
                else 
                {                    
                    response = Request.CreateResponse();                 
                    response.Content = new ObjectContent<Gender>(gender, fJsonMTF, C_MT_JSON);
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
                _genderBusiness.AddAsync(gender);
                await _genderBusiness.ApplyChagesAsync();

                response = Request.CreateResponse();
                response.Content = new ObjectContent<Gender>(gender, fJsonMTF, C_MT_JSON);
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
                    response.ReasonPhrase = "No gender found";
                }

                _genderBusiness.UpdateAsync(gender);
                await _genderBusiness.ApplyChagesAsync();

                response = Request.CreateResponse();
                response.Content = new ObjectContent<Gender>(gender, fJsonMTF, C_MT_JSON);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.ReasonPhrase = ExceptionUtils.GetErrorMessages(ex);
            }
            return response;
        }
        public async Task<HttpResponseMessage> Remove(int id)
        {
            HttpResponseMessage response;
            try
            {
                var gender = _genderBusiness.Get(id);
                if (gender == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = "No gender found";
                }

                _genderBusiness.DeleteAsync(id);
                await _genderBusiness.ApplyChagesAsync();

                response = Request.CreateResponse();
                response.Content = new StringContent("Gender deleted successfully");
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
