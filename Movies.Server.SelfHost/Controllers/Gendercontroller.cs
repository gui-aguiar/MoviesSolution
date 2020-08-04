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
    /// Gender controller Class. Exposes the Gender related APIs.
    /// </summary>
    [Authorize]
    public class Gendercontroller : ApiController
    {
        #region Fields
        
        private readonly GenderBusiness _genderBusiness;
        private static readonly JsonMediaTypeFormatter fJsonMTF = new JsonMediaTypeFormatter();

        #endregion

        #region Constructors
        public Gendercontroller()
        {
            _genderBusiness = new GenderBusiness();
        }
        #endregion

        #region Public methods
        
        /// <summary>
        /// List all Genders
        /// </summary>
        /// <returns>Json containing all Genders </returns>
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
                    response.ReasonPhrase = Consts.C_GENDER_NOT_FOUND;
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

        /// <summary>
        /// Retrieves specific Gender identified by its Id
        /// </summary>
        /// <param name="id">The Gender Id specified by an integer</param>
        /// <returns>Json containing the Gender info </returns>
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

        /// <summary>
        /// Creates and store a Gender
        /// </summary>
        /// <param name="gender">The Gender properties as JSon</param>
        /// <returns>Json containing the created Gender info </returns>
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

        /// <summary>
        /// Updates and store a Gender
        /// </summary>
        /// <param name="id">The Id of the Gender that will be updated</param>
        /// <param name="gender">The new Gender properties as JSon</param>
        /// <returns>Json containing the Gender info after the update</returns>
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

        /// <summary>
        /// Deletes a Gender specified by its Id
        /// </summary>
        /// <param name="id">The Id of the Gender that will be deleted</param>
        /// <returns>String message with the deletion status</returns>
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

        #endregion
    }
}
