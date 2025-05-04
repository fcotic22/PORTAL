using Data_Access_Layer;
using Data_Access_Layer.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Logic_Layer
{
    public class VehicleService
    {
        public List<Vehicle> GetAllVehicles()
        {
            using (var vehicleRepository = new VehicleRepository())
            {
                return vehicleRepository.GetAllVehicles();
            }
        }
    }
}
