using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class DoctorsShift
    {
        public Shift Shift { get; set; }
        public DateTime LastUpdated { get; set; }
        public Shift ChangingShift { get; set; }
        public int NumberOfFreeDays { get; set; }
        public DateTime FreeDaysBegin { get; set; }
        public List<ScheduledShift> ScheduledShifts { get; set; }

        public DoctorsShift()
        {
            ScheduledShifts = new List<ScheduledShift>();
        }
    }
}
