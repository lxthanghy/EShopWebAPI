using EShopWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopWebAPI.Controllers
{
    public class UserValidate
    {
        public static bool Login(string user, string pass)
        {
            using (EShopEntities db = new EShopEntities())
            {
                var account = db.Accounts.SingleOrDefault(u => u.Username == user &&
                u.Password == pass);
                if (account != null)
                    return true;
                else
                    return false;
            }
        }
    }
}