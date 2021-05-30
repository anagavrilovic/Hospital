using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class PatientNotesNotification:PatientNotification
    {
        private String times { get; set; }
        public String Times
        {
            get
            {
                return times;
            }
            set
            {
                times = value;
            }
        }
        private String name { get; set; }
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private String duration { get; set; }
        public String Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }

        private Boolean active { get; set; }
        public Boolean Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        private Boolean read { get; set; }
        public Boolean Read
        {
            get
            {
                return read;
            }
            set
            {
                read = value;
            }
        }
        public String Content { get; set; }
        
        public String ID { get; set; }
       public string patientID { get; set; }

       public Boolean[] Days { get; set; }
       public TimeSpan HoursMinutes { get; set; }

        public PatientNotesNotification(string name, string content, string iD, string patientID, Boolean[] days, TimeSpan hoursMinutes)
        {
            this.name = name;
            Content = content;
            ID = iD;
            this.patientID = patientID;
            Days = days;
            HoursMinutes = hoursMinutes;
            read = true;
            active = true;
            times = hoursMinutes.ToString();
            SetDurationBasedOnDays();
        }

        private void SetDurationBasedOnDays()
        {
            duration = "";
            if (!Days.Contains(true)) duration = "Samo jednom";
            else if (Days[0] == true) duration = duration + "Pon";
            else if (Days[1] == true) duration = duration + "Uto";
            else if (Days[2] == true) duration = duration + "Sre";
            else if (Days[3] == true) duration = duration + "Čet";
            else if (Days[4] == true) duration = duration + "Pet";
            else if (Days[5] == true) duration = duration + "Sub";
            else if (Days[6] == true) duration = duration + "Ned";
        }
    }
}
