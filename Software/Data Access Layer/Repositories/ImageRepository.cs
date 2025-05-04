using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class ImageRepository : Repository<Image> 
    {
        public ImageRepository() : base(new PORTAL_DBContext()) { }
        public override int Update(Image entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Image> GetLogo()
        {
            var query = from logo in Entities where logo.id == 1 select logo;
            return query;
        }
    }
}
