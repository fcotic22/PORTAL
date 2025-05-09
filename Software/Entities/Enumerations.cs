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
    }
}
