using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
   public class PatientNote
    {
        public string ID { get; set; }
        public String Subject { get; set; }
        public String Text { get; set; }
        public string patientID { get; set; }

        public PatientNote(string iD, string subject, string text, string patientID)
        {
            ID = iD;
            Subject = subject;
            Text = text;
            this.patientID = patientID;
        }
    }
}
