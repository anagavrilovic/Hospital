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

        public bool SaveAndCheck(Appointment p)
        {
            ObservableCollection<Appointment> appointment = GetAll();

            foreach (Appointment ap in appointment)
            {
                if (ap.DateTime < p.DateTime.AddHours(p.DurationInHours) && p.DateTime < ap.DateTime.AddHours(ap.DurationInHours))
                    return false;
            }

            appointment.Add(p);
            DoSerialization(appointment);
            return true;
        }

        public ObservableCollection<Appointment> GetByPatient(String id)
        {
            ObservableCollection<Appointment> apps = GetAll();
            ObservableCollection<Appointment> patientApps = new ObservableCollection<Appointment>();
            Boolean found = false;
            foreach (Appointment app in apps)
            {
                if (app.IDpatient.Equals(id))
                {
                    found = true;
                    patientApps.Add(app);
                }
            }

            if (found)
            {
                return patientApps;
            }
            else
            {
                return null;
            }
        }

        public Boolean Delete(string id)
        {
              ObservableCollection<Appointment> appointments = GetAll();
              foreach(Appointment r in appointments)
              {
                  if (r.IDAppointment.Equals(id))
                  {
                      appointments.Remove(r);
                      DoSerialization(appointments);
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

        public String GetNewID()
        {
            ObservableCollection<Appointment> apps;
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                apps = GetAll();
                apps = JsonConvert.DeserializeObject<ObservableCollection<Appointment>>(sr.ReadToEnd());
            }

            int retVal = 1;
            if (apps.Count == 0)
            {
                return retVal.ToString();
            }
            foreach (Appointment app in apps)
            {
                int x = Int32.Parse(app.IDAppointment);
                if (x >= retVal)
                {
                    retVal++;
                }
            }
            return retVal.ToString();
        }

        public ObservableCollection<Appointment> GetByDoctor(String id)
        {
            ObservableCollection<Appointment> apps = GetAll();
            ObservableCollection<Appointment> doctorApps = new ObservableCollection<Appointment>();
            Boolean found = false;
            foreach (Appointment app in apps)
            {
                if (app.IDDoctor.Equals(id))
                {
                    found = true;
                    doctorApps.Add(app);
                }
            }

            if (found)
            {
                return doctorApps;
            }
            else
            {
                return null;
            }
        }

    }
}