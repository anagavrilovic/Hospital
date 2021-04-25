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
    class PatientSettingsStorage
    {
        private String fileName;

        public PatientSettingsStorage(String file = "patientSettings.json")
        {
            this.fileName = file;

        }

        public ObservableCollection<PatientSettings> GetAll()
        {
            ObservableCollection<PatientSettings> patientSettings;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                patientSettings = (ObservableCollection<PatientSettings>)serializer.Deserialize(file, typeof(ObservableCollection<PatientSettings>));
            }

            return patientSettings;
        }

        public void DoSerialization(ObservableCollection<PatientSettings> patientSettings)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, patientSettings);
            }
        }

        public void Save(PatientSettings parameter1)
        {
            ObservableCollection<PatientSettings> patientSettings = GetAll();
            if (patientSettings == null)
            {
                patientSettings = new ObservableCollection<PatientSettings>();
            }
            patientSettings.Add(parameter1);
            DoSerialization(patientSettings);
        }

        public PatientSettings getByID(String id)
        {
            ObservableCollection<PatientSettings> patientSettings = GetAll();
            foreach (PatientSettings a in patientSettings)
            {
                if (a.IDPatient.Equals(id))
                {
                    return a;
                }
            }

            return null;
        }

        public Boolean Delete(string id)
        {
            ObservableCollection<PatientSettings> patientSettings = GetAll();
            if (patientSettings == null) return false; 
            foreach (PatientSettings r in patientSettings)
            {
                if (r.IDPatient.Equals(id))
                {
                    patientSettings.Remove(r);
                    DoSerialization(patientSettings);
                    return true;
                }
            }
            return false;
        }

        public Boolean isSchedulingAllowed()
        {
            PatientSettings patientSettings = getByID(MainWindow.IDnumber);
            if ((patientSettings.LatestScheduledAppointmentsTime == null) || (patientSettings.LatestScheduledAppointmentsTime.Count<3))
            {
                return true;
            }

            foreach(DateTime dts in patientSettings.LatestScheduledAppointmentsTime)
            {
                if ((DateTime.Now - dts).TotalDays > 10)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddScheduling(DateTime dt)
        {
            PatientSettings patientSettings = getByID(MainWindow.IDnumber);
           if(patientSettings.LatestScheduledAppointmentsTime == null)
            {
                patientSettings.LatestScheduledAppointmentsTime = new List<DateTime>();
            }
            patientSettings.LatestScheduledAppointmentsTime.Add(dt);
            if (patientSettings.LatestScheduledAppointmentsTime.Count > 3)
            {
                patientSettings.LatestScheduledAppointmentsTime.RemoveAt(0);
            }
            Delete(MainWindow.IDnumber);
            Save(patientSettings);
        }
    }
}
