using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
   public class HospitalizedPatient
    {
        private Patient patient;
        public Patient Patient
        {
            get { return patient; }
            set { patient = value; }
        }
        private Room room;
        public Room Room
        {
            get { return room; }
            set { room = value; }
        }
        private DateTime startOfTreatment;
        public DateTime StartOfTreatment
        {
            get { return startOfTreatment; }
            set { startOfTreatment = value; }
        }
        private DateTime endOfTreatment;
        public DateTime EndOfTreatment
        {
            get { return endOfTreatment; }
            set { endOfTreatment = value; }
        }
    }
}
