using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class PatientNotesNotification : IPatientNotification
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

       public DateTime LastRead { get; set; }

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
            LastRead = DateTime.Now;
        }

        private void SetDurationBasedOnDays()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!Days.Contains(true)) stringBuilder.Append("Samo jednom");
            if (Days[0] == true) stringBuilder.Append("Pon ");
            if (Days[1] == true) stringBuilder.Append("Uto ");
            if (Days[2] == true) stringBuilder.Append("Sre ");
            if (Days[3] == true) stringBuilder.Append("Cet ");
            if (Days[4] == true) stringBuilder.Append("Pet ");
            if (Days[5] == true) stringBuilder.Append("Sub ");
            if (Days[6] == true) stringBuilder.Append("Ned ");
            duration = stringBuilder.ToString();
        }
    }
}
