﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class GlassForProjectRepository : Repository<GlassForSubproject>
    {
        public GlassForProjectRepository() : base(new PORTAL_DBContext()) { }
        public override int Update(GlassForSubproject entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
