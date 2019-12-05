using FlightCenterProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FlightWebApplication.Controllers
{
    public class BasicAuthenticationCompanyAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //Checks whether user information has been entered
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden,
                    "You must send user name and password in basic authentication");
                return;
            }

            //Encoding the user name and password from base64
            string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
            string decodedAuthenticationToken = Encoding.UTF8.GetString(
                Convert.FromBase64String(authenticationToken));

            string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
            string username = usernamePasswordArray[0];
            string password = usernamePasswordArray[1];

            FacadeBase fb1 = FlyingCenterSystem.GetInstance().Login("admin", "9999", out LoginTokenBase token1);
            LoginToken<Administrator> administratorToken = (LoginToken<Administrator>)token1;
            ILoggedInAdministratorFacade administratorFacade = (ILoggedInAdministratorFacade)fb1;
            if (administratorFacade.CheckUserNameAndPwdAirline(administratorToken, username, password) == true)
            {
                FacadeBase fb = FlyingCenterSystem.GetInstance().Login(username, password, out LoginTokenBase token);
                LoginToken<AirlineCompany> airlineLoginToken = (LoginToken<AirlineCompany>)token;
                ILoggedInAirlineFacade airlineLoginFacade = (ILoggedInAirlineFacade)fb;
                actionContext.Request.Properties["airlineToken"] = airlineLoginToken;
                actionContext.Request.Properties["airlineFacade"] = airlineLoginFacade;
            }
            else
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                       "You are not authorized");
        }


    }   
}