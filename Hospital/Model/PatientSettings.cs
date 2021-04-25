using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class PatientSettings
    {
        public String ChosenDoctor {get;set;}
        public String IDPatient { get; set; }

        public List<DateTime> LatestScheduledAppointmentsTime { get; set; }
    }
}
