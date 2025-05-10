using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class EmployeeRepository : Repository<Employee>
    {
        public EmployeeRepository() : base(new PORTAL_DBContext()) { }
        public override int Update(Employee entity, bool saveChanges = true)
        {
            var employee = GetEmployeeById(entity.id).FirstOrDefault();
            employee.id = entity.id;
            employee.name = entity.name;
            employee.surname = entity.surname;
            employee.oib = entity.oib;
            employee.phone = entity.phone;
            employee.adress = entity.adress;
            employee.city = entity.city;
            employee.country = entity.country;
            employee.zipCode = entity.zipCode;
            employee.bankAccountNumber = entity.bankAccountNumber;
            employee.notes = entity.notes;
            employee.hourlyPay = entity.hourlyPay;
            employee.status = entity.status;
            employee.isOnLeave = entity.isOnLeave;
            
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public IQueryable<Employee> GetEmployeeById(int id)
        {
            var query = from employee in Entities
                        where employee.id == id
                        select employee;
            return query;
        }
    }
}
