using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class VehicleRepository : Repository<Vehicle>
    {
        public VehicleRepository() : base(new PORTAL_DBContext()) { }

        public List<Vehicle> GetAllVehicles()
        {
            return GetAll().ToList();
        }
        public override int Update(Vehicle entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
