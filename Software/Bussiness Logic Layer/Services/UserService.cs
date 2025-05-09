using Data_Access_Layer.Repositories;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Logic_Layer.Services
{
    public class UserService
    {
        public bool Login(string username, string password)
        {
            using (var repo = new UserRepository())
            {
                var user = repo.GetUserByUsername(username).FirstOrDefault();
                if (user != null && user.password.Equals(password))
                    return true;
                else
                    return false;
            }
        }
    }
}
