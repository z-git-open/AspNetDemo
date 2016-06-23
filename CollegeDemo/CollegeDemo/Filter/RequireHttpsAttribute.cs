using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CollegeDemo.Filter
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                var html = "<p>Https is required</p>";
                if(actionContext.Request.Method.Method == "GET")
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Found, html);

                    var uriBuilder = new UriBuilder(actionContext.Request.RequestUri);
                    uriBuilder.Scheme = Uri.UriSchemeHttps;
                    uriBuilder.Port = 443;
                    actionContext.Response.Headers.Location = uriBuilder.Uri;
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotFound, html);
                }
            }
        }
    }
}