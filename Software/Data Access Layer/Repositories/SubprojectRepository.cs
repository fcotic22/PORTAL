using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class SubprojectRepository : Repository<Subproject>
    {
        public SubprojectRepository() : base(new PORTAL_DBContext()) { }

        public override int Update(Subproject entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Subproject> GetSubprojectById(int id)
        {
            var query = from subproject in Entities
                        where subproject.id == id
                        select subproject;
            return query;
        }

        public IQueryable<Subproject> GetSubrojectByProjectIdAndSubprojectType(int project_id, int subproject_type)
        {
            var query = from subproject in Entities
                        where subproject.project_id == project_id && subproject.projectType == subproject_type
                        select subproject;
            return query;
        }
    }
}
