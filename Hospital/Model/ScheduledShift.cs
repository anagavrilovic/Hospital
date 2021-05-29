using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class ScheduledShift
    {
        public Shift Shift { get; set; }
        public DateTime Date { get; set; }
        public bool RollShifts { get; set; }
    }
}
