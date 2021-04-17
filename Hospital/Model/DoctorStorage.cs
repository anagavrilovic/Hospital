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
    class DoctorStorage
    {
        private String fileName;

        public DoctorStorage(String file = "doctors.json")
        {
            this.fileName = file;

        }

        public ObservableCollection<Doctor> GetAll()
        {
            ObservableCollection<Doctor> doctors;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                doctors = (ObservableCollection<Doctor>)serializer.Deserialize(file, typeof(ObservableCollection<Doctor>));
            }

            return doctors;
        }

        public void Save(Doctor parameter1)
        {
            ObservableCollection<Doctor> doctors = GetAll();
            doctors.Add(parameter1);
            DoSerialization(doctors);
        }

        /*public Boolean Delete(int id)
        {
              ObservableCollection<MedicalRecord> records = GetAll();
              foreach(MedicalRecord r in records)
              {
                  if (r.MedicalRecordID.Equals(id))
                  {
                      records.Remove(r);
                      DoSerialization(records);
                      return true;
                  }
              }
              return false;
        }*/

        public Doctor GetOne(string id)
        {
            ObservableCollection<Doctor> doctors = GetAll();
            foreach (Doctor d in doctors)
            {
                if (d.PersonalID.Equals(id))
                {
                    return d;
                }
            }

            return null;
        }

        public void DoSerialization(ObservableCollection<Doctor> doctors)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, doctors);
            }
        }
        public String GetByUsername(string username)
        {
            ObservableCollection<Doctor> doctors = GetAll();
            foreach (Doctor d in doctors)
            {
                if (d.Username.Equals(username))
                {
                    return d.PersonalID;
                }
            }

            return null;
        }

        public String GetIDByNameSurname(string nameSurname)
        {
            String[] nameSurnameSplitted = nameSurname.Split(' ');
            ObservableCollection<Doctor> doctors = GetAll();
            foreach (Doctor d in doctors)
            {
                if (d.FirstName.Equals(nameSurnameSplitted[0]) && d.LastName.Equals(nameSurnameSplitted[1]))
                {
                    return d.PersonalID;
                }
            }

            return null;
        }




    }
}

