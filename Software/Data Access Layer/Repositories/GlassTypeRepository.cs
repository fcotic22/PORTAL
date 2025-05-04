using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class GlassTypeRepository : Repository<GlassType>
    {
        public GlassTypeRepository() : base(new PORTAL_DBContext()) { }
        public override int Update(GlassType entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
