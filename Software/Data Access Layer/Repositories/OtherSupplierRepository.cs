using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    internal class OtherSupplierRepository : Repository<OtherSupplier>
    {
        public OtherSupplierRepository() : base(new PORTAL_DBContext()) { }
        public override int Update(OtherSupplier entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
