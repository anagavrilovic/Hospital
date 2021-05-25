using Hospital.Model;
using Hospital.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class PatientSettingsFileRepository : IPatientSettingsRepository
    {
        private string fileName = "patientSettings.json";

        public PatientSettingsFileRepository() { }
        public void Delete(string id)
        {
            List<PatientSettings> patientSettings = GetAll();
            if (patientSettings == null) return;
            foreach (PatientSettings r in patientSettings)
            {
                if (r.IDPatient.Equals(id))
                {
                    patientSettings.Remove(r);
                    Serialize(patientSettings);
                    return;
                }
            }
            
        }

        public List<PatientSettings> GetAll()
        {
            List<PatientSettings> patientSettings;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                patientSettings = (List<PatientSettings>)serializer.Deserialize(file, typeof(List<PatientSettings>));
            }

            return patientSettings;
        }

        public PatientSettings GetByID(string id)
        {
            List<PatientSettings> patientSettings = GetAll();
            foreach (PatientSettings a in patientSettings)
            {
                if (a.IDPatient.Equals(id))
                {
                    return a;
                }
            }

            return null;
        }

        public void Save(PatientSettings parameter)
        {
            List<PatientSettings> patientSettings = GetAll();
            if (patientSettings == null)
            {
                patientSettings = new List<PatientSettings>();
            }
            patientSettings.Add(parameter);
            Serialize(patientSettings);
        }

        public void Serialize(List<PatientSettings> parameter)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, parameter);
            }
        }

        public void Update(PatientSettings patientSettings)
        {
            Delete(MainWindow.IDnumber);
            Save(patientSettings);
        }

        public void FirstSave(String id)
        {
            PatientSettings patientSettings = new PatientSettings(id, "Nije mi bitno");
            Save(patientSettings);
        }
    }
}
