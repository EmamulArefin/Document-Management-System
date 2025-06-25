using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DocMS.Auth
{
    public class Logged : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var header = actionContext.Request.Headers.Authorization;
            if (header == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "No token supplied");
            }
            else
            {
                var tokenKey = header.Parameter; // ✅ this gets the actual token only
                if (!AuthService.IsTokenValid(tokenKey))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "Supplied token is invalid or expired");
                }
            }

            base.OnAuthorization(actionContext);
        }

    }
}