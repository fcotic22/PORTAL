using Data_Access_Layer.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Logic_Layer.Services
{
    public class SubprojectService
    {
        public static Subproject GetSubrojectByProjectIdAndSubprojectType(int project_id, int subproject_type)
        {
            using (var repo = new SubprojectRepository())
            {
                return repo.GetSubrojectByProjectIdAndSubprojectType(project_id, subproject_type).FirstOrDefault();
            }
        }
    }
}
