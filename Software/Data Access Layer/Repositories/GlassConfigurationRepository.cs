using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class GlassConfigurationRepository : Repository<GlassConfiguration>
    {
        public GlassConfigurationRepository() : base(new PORTAL_DBContext()) { }
        public override int Update(GlassConfiguration entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
