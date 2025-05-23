using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class Enumerations
    {
        public enum Priority
        {
            Low, Medium, High
        }

        public enum Status
        {
            Open, InProgress, Closed, 
        }

        public enum StatusOfEmployment
        {
            Otpušten, Stalno, Ispomoć, Bolovanje
        }
        public enum ProjectType
        {
            PVC, ALU, FACADE 
        }
    }
}
