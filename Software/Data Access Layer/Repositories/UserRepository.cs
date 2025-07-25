﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class UserRepository : Repository<User> 
    {
        public UserRepository() : base(new PORTAL_DBContext()) { }
        public override int Update(User entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetUserByUsername(string username) 
        { 
            var query = from user in Entities where user.username == username select user;
            return query;
        }
    }
}
