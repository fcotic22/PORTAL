using Data_Access_Layer.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Logic_Layer.Services
{
    public class LeaveService
    {
        public bool AddNewLeave(Leave leave)
        {
            using (var repo = new LeaveRepository())
            {
                if (repo.Add(leave) != 0) return true;
                else return false;
            }
        }

        public void UpdateLeave(Leave leave)
        {
            using (var repo = new LeaveRepository())
            {
                repo.Update(leave);
            }
        }
        public void DeleteLeave(Leave leave) 
        {
            using (var repo = new LeaveRepository())
            {
                repo.Remove(leave);
            }
        }

        public void RemoveAllLeavesForEmployeeId(int id)
        {
            using (var repo = new LeaveRepository())
            {
                var leavesToRemove = repo.GetAll()
                                         .Where(leave => leave.employee_id == id)
                                         .ToList();

                foreach (var leave in leavesToRemove)
                {
                    repo.Remove(leave);
                }
            }
        }

        public Leave GetCurrentLeaveForEmployeeId(int id)
        {
            using (var repo = new LeaveRepository())
            {
                return repo.GetAll()
                           .FirstOrDefault(leave => leave.employee_id == id && leave.endDate == null);
            }
        }
    }
}
