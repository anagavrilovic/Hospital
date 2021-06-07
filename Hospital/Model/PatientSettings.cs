using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class PatientSettings
    {
        public String ChosenDoctor {get;set;}
        public String IDPatient { get; set; }

        public List<DateTime> LatestScheduledAppointmentsTime { get; set; }
        public Boolean ShowWizard { get; set; }

        public Boolean IsFirstLogin { get; set; }
        public Boolean ShowTooltips { get; set; }

        [JsonIgnore]
        public MedicalRecord PatientsRecord { get; set; }

        public PatientSettings(string chosenDoctor, string iDPatient)
        {
            ChosenDoctor = chosenDoctor;
            IDPatient = iDPatient;
        }


    }
}
