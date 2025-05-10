using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class LeaveRepository : Repository<Leave>
    {
        public LeaveRepository() : base(new PORTAL_DBContext()){}

        public override int Update(Leave entity, bool saveChanges = true)
        {
            var leave = GetLeaveById(entity.id).FirstOrDefault();
            

            leave.startDate = entity.startDate;
            leave.endDate = entity.endDate;
            leave.reasonForLeave = entity.reasonForLeave;
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public IQueryable<Leave> GetLeaveById(int id)
        {
            var query = from leave in Entities
                        where leave.id == id
                        select leave;
            return query;
        }
    }
}
