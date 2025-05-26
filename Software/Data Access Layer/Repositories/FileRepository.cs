using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Entities.Models.File;

namespace Data_Access_Layer.Repositories
{
    public class FileRepository : Repository<File> 
    {
        public FileRepository() : base(new PORTAL_DBContext()) { }

        public IQueryable<File> GetFilesByProjectId(int id)
        {
            var query = from files in Entities where files.project_id == id select files;
            return query;
        }

        public override int Update(File entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
