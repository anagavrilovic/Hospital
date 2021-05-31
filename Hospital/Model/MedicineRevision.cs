using Newtonsoft.Json;

namespace Hospital.Model
{
    public class MedicineRevision
    {
        public Medicine Medicine { get; set; }

        public string DoctorID { get; set; }

        [JsonIgnore]
        public Doctor RevisionDoctor { get; set; }

        public string RevisionExplanation { get; set; }

        public bool IsMedicineRevised { get; set; }
        [JsonIgnore]
        public string RevisionStatus { get; set; }
    }
}
