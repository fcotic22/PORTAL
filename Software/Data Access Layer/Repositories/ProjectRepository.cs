using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class ProjectRepository : Repository<Project>
    {
        public ProjectRepository() : base(new PORTAL_DBContext()) {}

        public override int Update(Project entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
