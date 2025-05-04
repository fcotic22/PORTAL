using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class FacadeProjectRepository : Repository<FacadeProject> 
    {
        public FacadeProjectRepository() : base(new PORTAL_DBContext()) { }
        public override int Update(FacadeProject entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
