using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EShopWebAPI.Controllers
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public const string Realm = "My Realm";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response =
                actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                if (actionContext.Response.StatusCode ==
                System.Net.HttpStatusCode.Unauthorized)
                    actionContext.Request.Headers.Add("WWW-Authenticate",
                    string.Format("Basic Realm = \"{0}\"", Realm));
            }
            else
            {
                string token = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken =
                Encoding.UTF8.GetString(Convert.FromBase64String(token));
                string[] user_pass = decodedToken.Split(':');
                string user = user_pass[0];
                string pass = user_pass[1];
                if (UserValidate.Login(user, pass))
                {
                    var identity = new GenericIdentity(user);
                    IPrincipal principal = new GenericPrincipal(identity, null);
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                        HttpContext.Current.User = principal;
                }
                else
                {
                    actionContext.Response =
                    actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}