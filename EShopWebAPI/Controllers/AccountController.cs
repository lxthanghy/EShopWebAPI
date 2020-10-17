using EShopWebAPI.Models;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EShopWebAPI.Controllers
{
    [BasicAuthentication]
    public class AccountController : ApiController
    {
        private static EShopEntities eShopEntities;

        public AccountController()
        {
            eShopEntities = new EShopEntities();
        }

        [HttpGet]
        public HttpResponseMessage GetPermissionsByRole(string roleName)
        {
            var funtionNames = from r in eShopEntities.Roles
                               where r.Name.Contains(roleName)
                               join p in eShopEntities.Permissions on r.RoleID equals p.RoleID
                               join f in eShopEntities.Functions on p.FunctionID equals f.FunctionID
                               select f.FunctionName;
            return Request.CreateResponse(HttpStatusCode.OK, funtionNames);
        }

        [HttpGet]
        public HttpResponseMessage GetPermissionsByUser(string username)
        {
            var funtionNames = from a in eShopEntities.Accounts
                               where a.Username.Equals(username)
                               join r in eShopEntities.Roles on a.RoleID equals r.RoleID
                               join p in eShopEntities.Permissions on r.RoleID equals p.RoleID
                               join f in eShopEntities.Functions on p.FunctionID equals f.FunctionID
                               select f.FunctionName;
            return Request.CreateResponse(HttpStatusCode.OK, funtionNames);
        }
    }
}