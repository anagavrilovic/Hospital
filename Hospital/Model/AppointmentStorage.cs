using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Hospital
{

   public class AppointmentStorage
   {

        private String fileName;

        public AppointmentStorage(String file = "appointments.json")
        {
            this.fileName = file;

        }

        public ObservableCollection<Appointment> GetAll()
        {
            ObservableCollection<Appointment> appointment;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                appointment = (ObservableCollection<Appointment>)serializer.Deserialize(file, typeof(ObservableCollection<Appointment>));
            }

            return appointment;
        }

        public void Save(Appointment parameter1)
        {
            ObservableCollection<Appointment> appointment = GetAll();
            appointment.Add(parameter1);
            DoSerialization(appointment);
        }

        internal ObservableCollection<Appointment> GetByPatient(string v)
        {
            ObservableCollection<Appointment> appointments = GetAll();
            ObservableCollection<Appointment> ret = new ObservableCollection<Appointment>();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.IDpatient.Equals(v))
                {
                    ret.Add(appointment);
                }
            }
            return ret;
        }

        public Boolean Delete(string id)
        {
              ObservableCollection<Appointment> records = GetAll();
              foreach(Appointment r in records)
              {
                  if (r.IDAppointment.Equals(id))
                  {
                      records.Remove(r);
                      DoSerialization(records);
                      return true;
                  }
              }
              return false;
        }

        public Appointment GetOne(string id)
        {
            ObservableCollection<Appointment> appointment = GetAll();
            foreach (Appointment a in appointment)
            {
                if (a.IDAppointment.Equals(id))
                {
                    return a;
                }
            }

            return null;
        }

        public void DoSerialization(ObservableCollection<Appointment> appointment)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, appointment);
            }
        }
    }
}