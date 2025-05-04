using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class PanelRepository : Repository<Panel>
    {
        public PanelRepository() : base(new PORTAL_DBContext()) { }
        public override int Update(Panel entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
