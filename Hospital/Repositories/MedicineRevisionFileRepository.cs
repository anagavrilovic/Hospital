﻿using Hospital.Model;
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
    public class MedicineRevisionFileRepository : IMedicineRevisionRepository
    {
        private string fileName = "medicineRevision.json";

        public MedicineRevisionFileRepository() { }

        public void Delete(string medicineID)
        {
            ObservableCollection<MedicineRevision> medicineRevisions = GetAll();
            foreach (MedicineRevision medicineRevision in medicineRevisions)
            {
                if (medicineRevision.Medicine.ID.Equals(medicineID))
                {
                    medicineRevisions.Remove(medicineRevision);
                    Serialize(medicineRevisions);
                    return;
                }
            }
        }

        public ObservableCollection<MedicineRevision> GetAll()
        {
            ObservableCollection<MedicineRevision> medicineRevisions = new ObservableCollection<MedicineRevision>();

            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                medicineRevisions = JsonConvert.DeserializeObject<ObservableCollection<MedicineRevision>>(sr.ReadToEnd());

                if (medicineRevisions == null)
                    return new ObservableCollection<MedicineRevision>();
            }

            return medicineRevisions;
        }

        public MedicineRevision GetByID(string medicineID)
        {
            ObservableCollection<MedicineRevision> medicineRevisions = GetAll();

            foreach (MedicineRevision medicineRevision in medicineRevisions)
                if (medicineRevision.Medicine.ID.Equals(medicineID))
                    return medicineRevision;

            return null;
        }

        public void Save(MedicineRevision medicineRevision)
        {
            ObservableCollection<MedicineRevision> medicineRevisions = GetAll();
            medicineRevisions.Add(medicineRevision);
            Serialize(medicineRevisions);
        }

        public void EditMedicine(MedicineRevision editedMedicineRevision)
        {
            ObservableCollection<MedicineRevision> medicineRevisions = GetAll();
            foreach (MedicineRevision medicineRevision in medicineRevisions.ToList())
            {
                if (editedMedicineRevision.Medicine.ID.Equals(medicineRevision.Medicine.ID))
                {
                    medicineRevisions.Remove(medicineRevision);
                    medicineRevisions.Add(editedMedicineRevision);
                    Serialize(medicineRevisions);
                    break;
                }
            }
        }

        public void Serialize(ObservableCollection<MedicineRevision> medicineRevisions)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, medicineRevisions);
            }
        }
    }
}
