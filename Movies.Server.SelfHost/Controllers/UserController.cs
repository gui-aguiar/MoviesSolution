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
    /// User controller Class. Exposes the User related APIs.
    /// The only methods allowes are to the the Users list and to crate a new user 
    /// </summary>
    public class UserController : ApiController
    {
        #region Fields
        private readonly UserBusiness _userBusiness;
        private static readonly JsonMediaTypeFormatter fJsonMTF = new JsonMediaTypeFormatter();
        #endregion

        #region Constructors
        public UserController()
        {
            _userBusiness = new UserBusiness();
        }
        #endregion

        #region Public methods
        /// <summary>
        /// List all User Ids and Logins. The password is hidden by security reasons
        /// </summary>
        /// <returns>Json containing all Users Ids and Login </returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                var users = _userBusiness.List();
                if (!users.Any())
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = Consts.C_USER_NOT_FOUND;
                }
                else
                {
                    var usersInfoList = _userBusiness.List().Select(u => new
                   {
                       u.Id,
                       u.Login
                   });
                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<IEnumerable<dynamic>>(usersInfoList, fJsonMTF, Consts.C_MT_JSON);
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
        /// Creates and store a User
        /// </summary>
        /// <param name="user">The User properties as JSon, containing its Login and Password as string</param>
        /// <returns>Json containing the created User info </returns>
        [HttpPost]
        public async Task<HttpResponseMessage> AddAsync(User user)
        {
            HttpResponseMessage response;
            try
            {
                if(_userBusiness.ValidateUser(user))
                {
                    _userBusiness.Add(user);
                    await _userBusiness.ApplyChagesAsync();

                    response = Request.CreateResponse();
                    response.Content = new ObjectContent<User>(user, fJsonMTF, Consts.C_MT_JSON);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.ReasonPhrase = Consts.VALIDATION_ERROR_RESPONSE_PHRASE;
                    response.Content = new StringContent(Consts.INVALID_USER_DATA);
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
