using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    internal class CSiteTaskRepository : Repository<CSiteTask>
    {
        public CSiteTaskRepository() : base(new PORTAL_DBContext()) { }
        public override int Update(CSiteTask entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
