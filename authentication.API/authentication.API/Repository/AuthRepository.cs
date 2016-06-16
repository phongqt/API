using authentication.API.Context;
using authentication.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace authentication.API.Repository
{
    public class AuthRepository : IDisposable
    {
        private BlogContext blg;
        public bool Login(string userName, string password, ref string message)
        {
            if (string.IsNullOrEmpty(userName))
            {
                message = "User name is required";
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                message = "Password is required";
                return false;
            }
            using (blg = new BlogContext())
            {
                var user = blg.Users.FirstOrDefault(x => x.UserName == userName && x.Password == password);
                if (user != null)
                {
                    return true;
                }
                return false;
            }
        }

        public void Dispose()
        {
            blg.Dispose();

        }
    }
}