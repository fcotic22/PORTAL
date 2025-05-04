using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Data_Access_Layer.Repositories
{
    public class TaskRepository : Repository<Entities.Models.Task>
    {
        public TaskRepository() : base(new PORTAL_DBContext()){}

        public override int Update(Entities.Models.Task entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
