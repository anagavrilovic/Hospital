﻿using Hospital.Model;
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
    public class HospitalTreatmentFileRepository : IFileRepository<HospitalTreatment>
    {
        private string fileName = "hospitalTreatment.json";

        public HospitalTreatmentFileRepository() { }

        public void Delete(string id)
        {
            ObservableCollection<HospitalTreatment> hospitalTreatments = GetAll();
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

        public ObservableCollection<HospitalTreatment> GetAll()
        {
            ObservableCollection<HospitalTreatment> hospitalTreatments;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                hospitalTreatments = (ObservableCollection<HospitalTreatment>)serializer.Deserialize(file, typeof(ObservableCollection<HospitalTreatment>));
            }

            if (hospitalTreatments == null)
                hospitalTreatments = new ObservableCollection<HospitalTreatment>();

            return hospitalTreatments;
        }

        public HospitalTreatment GetByID(string id)
        {
            ObservableCollection<HospitalTreatment> hospitalTreatments = GetAll();
            foreach (HospitalTreatment treatment in hospitalTreatments)
                if (treatment.PatientId.Equals(id))
                    return treatment;

            return null;
        }

        public void Save(HospitalTreatment hospitalTreatment)
        {
            ObservableCollection<HospitalTreatment> hospitalTreatments = GetAll();
            hospitalTreatments.Add(hospitalTreatment);
            Serialize(hospitalTreatments);
        }

        public void Serialize(ObservableCollection<HospitalTreatment> hospitalTreatments)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, hospitalTreatments);
            }
        }
    }
}
