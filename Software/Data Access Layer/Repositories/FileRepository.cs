using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class FileRepository : Repository<Entities.Models.File> 
    {
        public FileRepository() : base(new PORTAL_DBContext()) { }
        public override int Update(Entities.Models.File entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
