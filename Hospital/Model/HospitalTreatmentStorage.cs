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
    class HospitalTreatmentStorage
    {
        private String fileName;

        public HospitalTreatmentStorage(String file = "hospitalTreatment.json")
        {
            this.fileName = file;

        }

        public ObservableCollection<HospitalTreatment> GetAll()
        {
            ObservableCollection<HospitalTreatment> hospitalTreatments;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                hospitalTreatments = (ObservableCollection<HospitalTreatment>)serializer.Deserialize(file, typeof(ObservableCollection<HospitalTreatment>));
            }

            return hospitalTreatments;
        }

        public void Save(HospitalTreatment hospitalTreatment)
        {
            ObservableCollection<HospitalTreatment> hospitalTreatments = GetAll();
            hospitalTreatments.Add(hospitalTreatment);
            DoSerialization(hospitalTreatments);
        }

        public Boolean DeleteByPatientId(string id)
        {
              ObservableCollection<HospitalTreatment> hospitalTreatments = GetAll();
              foreach(HospitalTreatment treatment in hospitalTreatments)
              {
                  if (treatment.PatientId.Equals(id))
                  {
                    hospitalTreatments.Remove(treatment);
                      DoSerialization(hospitalTreatments);
                      return true;
                  }
              }
              return false;
        }

        public HospitalTreatment GetByPatientId(string id)
        {
            ObservableCollection<HospitalTreatment> hospitalTreatments = GetAll();
            foreach (HospitalTreatment treatment in hospitalTreatments)
            {
                if (treatment.PatientId.Equals(id))
                {
                    return treatment;
                }
            }

            return null;
        }

        public void DoSerialization(ObservableCollection<HospitalTreatment> hospitalTreatment)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, hospitalTreatment);
            }
        }
    }
}
