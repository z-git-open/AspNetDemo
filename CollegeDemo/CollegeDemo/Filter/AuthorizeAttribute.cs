using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CollegeDemo.Filter
{
    public class AuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null)
            {
                // if the authentication header of the request is not Null, 
                // that means the client request includes auth info (credentials), and he actually wants to authenticate
                if(authHeader.Scheme.Equals("basic", StringComparison.InvariantCultureIgnoreCase)
                    && !string.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    // ensure the client using basic authentication and parameter should contain his credential.
                    var rawCredentials = authHeader.Parameter; //it's base64 codeded string
                    var encoding = Encoding.GetEncoding("iso-8859-1"); // assure everyone follows same rule.
                    
                    // to get the real credential string formatted as "<username>:<password>"
                    var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials)); // real credential string
                    var tokens = credentials.Split(new char[] {':' });
                    //since here the client needs to send credential to the server, normally we require HTTPS for this call.    
                    var username = tokens[0];
                    var password = tokens[1];

                    // then validate username and password
                    if (ValidateCredential(username, password))
                    {
                        // the reason why setting thread principal is because other services may need to verify the identify
                        // using the principal, so we need to set it.
                        var principal = new GenericPrincipal(new GenericIdentity(username), null);
                        Thread.CurrentPrincipal = principal;

                        // only every check has been verified then it returns fine. 
                        // otherwise all other cases go to the final create unauthorized response.
                        return; 
                    }
                }
            }
            // if the authentication header of the request is NULL, meaning the client request does not include auth info (no credentials), 
            // and he did not want to autheticate. if we need to authenticate the client, then we should tell him please send your auth info over to the server.

            // only the happy path (inside the if) returns normally, for other cases we always create a unauthorized response for this requst.
            // so the pipeline knows something goes wrong, and then it stops.
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

            // we might need to tell the API caller how to authorize however still many browers or clients may not know how to deal with this.
            // but in case developers of client know, then it may be useful.
            actionContext.Response.Headers.Add("www-Authenticate", "Basic Scheme='CollegeDemo' Location='http://myauthlink'");
        }

        private bool ValidateCredential(string username, string password)
        {
            return true;
        }
    }
}