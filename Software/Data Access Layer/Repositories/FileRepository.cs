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

        public IQueryable<File> GetFilesByNameAndType(string name, string type)
        {
            var query = from file in Entities where file.name == name && file.fileType == type select file;
            return query;
        }

        public IQueryable<File> GetFilesByProjectId(int id)
        {
            var query = from files in Entities where files.project_id == id select files;
            return query;
        }

        public IQueryable<File> GetFileById(int id)
        {
            var query = from file in Entities where file.id == id select file;
            return query;
        }

        public override int Update(File entity, bool saveChanges = true)
        {
            var file = GetFileById(entity.id).FirstOrDefault();

            file.project_id = entity.project_id;
            file.name = entity.name;
            file.description = entity.description;
            file.uploadDate = entity.uploadDate;
            file.filePath = entity.filePath;
            file.fileType = entity.fileType;

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
    }
}
