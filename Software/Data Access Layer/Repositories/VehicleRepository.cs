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

        public IQueryable<Vehicle> GetAllVehicles()
        {
            return GetAll();
        }

        public IQueryable<Vehicle> GetVehicleById(int id)
        {
            var query = from vehicle in Entities where vehicle.id == id select vehicle;
            return query;
        }
        public override int Update(Vehicle entity, bool saveChanges = true)
        {
            var vehicle = GetVehicleById(entity.id).FirstOrDefault();
            vehicle.id = entity.id;
            vehicle.name = entity.name;
            vehicle.model = entity.model;
            vehicle.licensePlate = entity.licensePlate;
            vehicle.registrationValidTo = entity.registrationValidTo;
            vehicle.noOfSeats = entity.noOfSeats;
            vehicle.manufacturer = entity.manufacturer;
            vehicle.fuelType = entity.fuelType;
            vehicle.productionYear = entity.productionYear;
            vehicle.isCurrentlyAssigned = entity.isCurrentlyAssigned;
            vehicle.assignedTo = entity.assignedTo;
            vehicle.VehicleAssignmentCSites = entity.VehicleAssignmentCSites;

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
