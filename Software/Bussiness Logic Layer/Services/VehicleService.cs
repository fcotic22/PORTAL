using Data_Access_Layer;
using Data_Access_Layer.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Logic_Layer.Services
{
    public class VehicleService
    {
        public List<Vehicle> GetAllVehicles()
        {
            using (var vehicleRepository = new VehicleRepository())
            {
                return vehicleRepository.GetAllVehicles().ToList();
            }
        }

        public bool AddNewVehicle(Vehicle vehicle)
        {
            using (var vehicleRepository = new VehicleRepository())
            {
                if (vehicleRepository.Add(vehicle) != 0) return true;
                else return false;
            }
        }
        public void RemoveVehicle(Vehicle vehicle)
        {
            using (var vehicleRepository = new VehicleRepository())
            {
                vehicleRepository.Remove(vehicle);
            }
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            using (var vehicleRepository = new VehicleRepository())
            {
                vehicleRepository.Update(vehicle);
            }
        }
    }
}
