using Data_Access_Layer.Repositories;
using Entities.Models;
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

        public List<Employee> GetAllEmployees() 
        {
            using (var repo = new EmployeeRepository())
            {
                return repo.GetAll().ToList();
            }
        }

        public void RemoveEmployee(Employee employee)
        {
            using (var repo = new EmployeeRepository())
            {
                repo.Remove(employee);
            }
        }

        public bool AddNewEmployee(Employee employee)
        {
            using (var repo = new EmployeeRepository())
            {
                if (repo.Add(employee) != 0) return true;
                else return false;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (var repo = new EmployeeRepository())
            {
                repo.Update(employee);
            }
        }
    }
}
