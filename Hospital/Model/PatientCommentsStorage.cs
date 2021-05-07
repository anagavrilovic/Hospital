using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class PatientCommentsStorage
    {
        private String fileName;

        public PatientCommentsStorage(String file = "patientComments.json")
        {
            this.fileName = file;

        }

        public ObservableCollection<PatientComment> GetAll()
        {
            ObservableCollection<PatientComment> patientComments;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                patientComments = (ObservableCollection<PatientComment>)serializer.Deserialize(file, typeof(ObservableCollection<PatientComment>));
            }

            return patientComments;
        }

        public void DoSerialization(ObservableCollection<PatientComment> patientComments)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, patientComments);
            }
        }

        public void Save(PatientComment parameter1)
        {
            ObservableCollection<PatientComment> patientComments = GetAll();
            if (patientComments == null)
            {
                patientComments = new ObservableCollection<PatientComment>();
            }
            patientComments.Add(parameter1);
            DoSerialization(patientComments);
        }

        public Boolean IsAppointmentAlreadyGraded(String IDAppointment)
        {
            ObservableCollection<PatientComment> patientComments = GetAll();
            if (patientComments == null) return false;
            foreach (PatientComment a in patientComments)
            {
                if (a.IDAppointment.Equals(IDAppointment))
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean IsTimeForHospitalRating()
        {
            ObservableCollection<PatientComment> patientComments = GetAll();
            if (patientComments == null) return true;
            foreach (PatientComment a in patientComments)
            {
                if (a.IDPatient.Equals(MainWindow.IDnumber) && String.IsNullOrEmpty(a.IDAppointment))
                {
                    if ((a.DateWhenRated - DateTime.Now).TotalDays < 150) return false;
                }
            }
            return true;

        }

    }
}
