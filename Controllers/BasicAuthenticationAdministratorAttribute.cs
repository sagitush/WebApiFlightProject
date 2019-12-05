using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FlightCenterProject;


namespace FlightWebApplication.Controllers
{
    public class BasicAuthenticationAdministratorAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden,
                    "You must send user name and password in basic authentication");
                return;
            }
            string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
            string decodedAuthenticationToken = Encoding.UTF8.GetString(
                Convert.FromBase64String(authenticationToken));

            string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
            string username = usernamePasswordArray[0];
            string password = usernamePasswordArray[1];

            if (username == "admin" && password == "9999")
            {               
                FacadeBase fb = FlyingCenterSystem.GetInstance().Login(username, password, out LoginTokenBase token);
                actionContext.Request.Properties["administratorToken"] = token;
                LoggedInAdministratorFacade administratorFacade = (LoggedInAdministratorFacade)fb;
                actionContext.Request.Properties["administratorFacade"] = administratorFacade;
            }
            else
            {              
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                    "You are not authorized");
            }
        }
    }
}