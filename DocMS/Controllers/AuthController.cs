using BLL.Services;
using DocMS.Auth;
using DocMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DocMS.Controllers
{
    [EnableCors("*", "*", "*")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("api/login")]
        public HttpResponseMessage Auth(LoginModel login)
        {
            var tk = AuthService.Auth(login.Email, login.Password);
            if (tk != null)
            {
                HttpContext.Current.Session["UserId"] = tk.Id;
                return Request.CreateResponse(HttpStatusCode.OK, tk);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Credintials");
            }
        }

        [Logged]
        [HttpPost]
        [Route("api/logout")]
        public HttpResponseMessage Logout()
        {
            if (Request.Headers.Authorization == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Missing authorization token.");
            }
            var tokenKey = Request.Headers.Authorization.Parameter;
            var result = AuthService.Logout(tokenKey);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid or expired token.");
            }
        }

    }
}
