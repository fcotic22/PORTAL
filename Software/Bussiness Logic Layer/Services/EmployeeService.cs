using Data_Access_Layer.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Logic_Layer.Services
{
    public class EmployeeService
    {
        public List<string> GetAllEmployeeNames()
        {
            using (var repo = new EmployeeRepository())
            {
                var employees = repo.GetAll();
                List<string> employeeNames = new List<string>();
                foreach (var employee in employees)
                {
                    employeeNames.Add(employee.name + " " + employee.surname);
                }
                return employeeNames;
            }
        }
    }
}
