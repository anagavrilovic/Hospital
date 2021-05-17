using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class HospitalTreatment
    {
        private string patientId;
        private DateTime startOfTreatment;
        private DateTime endOfTreatment;
        private string roomId;
        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }
        public string RoomId
        {
            get { return roomId; }
            set { roomId = value; }
        }
        public DateTime StartOfTreatment
        {
            get { return startOfTreatment; }
            set { startOfTreatment = value; }
        }
        public DateTime EndOfTreatment
        {
            get { return endOfTreatment; }
            set { endOfTreatment = value; }
        }

        [JsonIgnore]
        public MedicalRecord PatientsRecord { get; set; }
        [JsonIgnore]
        public Room Room { get; set; }
    }
}
