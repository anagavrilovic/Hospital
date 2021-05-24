using Hospital.Model;
using Newtonsoft.Json;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class HospitalTreatmentFileRepository : IHospitalTreatmentRepository
    {
        private string fileName = "hospitalTreatment.json";

        public HospitalTreatmentFileRepository() { }

        public void Delete(string id)
        {
            List<HospitalTreatment> hospitalTreatments = GetAll();
            foreach (HospitalTreatment treatment in hospitalTreatments)
            {
                if (treatment.PatientId.Equals(id))
                {
                    hospitalTreatments.Remove(treatment);
                    Serialize(hospitalTreatments);
                    return;
                }
            }
        }

        public void EditHospitalTreatment(HospitalTreatment hospitalTreatment)
        {
            List<HospitalTreatment> hospitalTreatments = GetAll();
            foreach (HospitalTreatment h in hospitalTreatments)
            {
                if (h.PatientId.Equals(hospitalTreatment.PatientId))
                {
                    h.DeepCopy(hospitalTreatment);
                    break;
                }
            }
            Serialize(hospitalTreatments);
        }

        public List<HospitalTreatment> GetAll()
        {
            List<HospitalTreatment> hospitalTreatments;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                hospitalTreatments = (List<HospitalTreatment>)serializer.Deserialize(file, typeof(List<HospitalTreatment>));
            }

            if (hospitalTreatments == null)
                hospitalTreatments = new List<HospitalTreatment>();

            return hospitalTreatments;
        }

        public HospitalTreatment GetByID(string id)
        {
            List<HospitalTreatment> hospitalTreatments = GetAll();
            foreach (HospitalTreatment treatment in hospitalTreatments)
                if (treatment.PatientId.Equals(id))
                    return treatment;

            return null;
        }

        public void Save(HospitalTreatment hospitalTreatment)
        {
            List<HospitalTreatment> hospitalTreatments = GetAll();
            hospitalTreatments.Add(hospitalTreatment);
            Serialize(hospitalTreatments);
        }

        public void Serialize(List<HospitalTreatment> hospitalTreatments)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, hospitalTreatments);
            }
        }
    }
}
