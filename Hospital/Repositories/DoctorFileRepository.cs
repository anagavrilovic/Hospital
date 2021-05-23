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
    public class DoctorFileRepository : IDoctorRepository
    {
        private string fileName = "doctors.json";

        public void Delete(string id)
        {
            ObservableCollection<Doctor> doctors = GetAll();
            foreach (Doctor d in doctors)
            {
                if (d.PersonalID.Equals(id))
                {
                    doctors.Remove(d);
                    Serialize(doctors);
                    return;
                }
            }
        }

        public ObservableCollection<Doctor> GetAll()
        {
            ObservableCollection<Doctor> doctors;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                doctors = (ObservableCollection<Doctor>)serializer.Deserialize(file, typeof(ObservableCollection<Doctor>));
            }

            if (doctors == null)
                doctors = new ObservableCollection<Doctor>();

            return doctors;
        }

        public Doctor GetByID(string id)
        {
            ObservableCollection<Doctor> doctors = GetAll();
            foreach (Doctor d in doctors)
                if (d.PersonalID.Equals(id))
                    return d;

            return null;
        }

        public void Save(Doctor doctor)
        {
            ObservableCollection<Doctor> doctors = GetAll();
            doctors.Add(doctor);
            Serialize(doctors);
        }

        public void Serialize(ObservableCollection<Doctor> doctors)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, doctors);
            }
        }
    }
}
