using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class PatientComment
    {
       public String ID { get; set; }
       public String IDPatient { get; set; }
       public String IDAppointment { get; set; }
       public String Comment { get; set; }
       public int Grade { get; set; }
       public String DoctorsNameSurname { get; set; }

        public DateTime DateWhenRated { get; set; }

        public PatientComment()
        {

        }
        public PatientComment(string id, string iDPatient, string iDAppointment, string comment, int grade, string doctorsNameSurname, DateTime dateWhenRated)
        {
            ID = id;
            IDPatient = iDPatient;
            IDAppointment = iDAppointment;
            Comment = comment;
            Grade = grade;
            DoctorsNameSurname = doctorsNameSurname;
            DateWhenRated = dateWhenRated;
        }

        public PatientComment(string id, string iDPatient, string comment, int grade, DateTime dateWhenRated)
        {
            ID = id;
            IDPatient = iDPatient;
            IDAppointment = "";
            Comment = comment;
            Grade = grade;
            DoctorsNameSurname = "";
            DateWhenRated = dateWhenRated;
        }

        [JsonIgnore]
        public Appointment Appointment { get; set; }
        [JsonIgnore]
        public MedicalRecord PatientsRecord { get; set; }
    }
}
