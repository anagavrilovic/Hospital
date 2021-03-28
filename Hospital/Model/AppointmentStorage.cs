using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Hospital
{

   public class AppointmentStorage
   {

      public AppointmentStorage()
        {
            this.fileName = "appointments.json";
           
        }

      
      public ObservableCollection<Appointment> GetAll()
      {
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                //JsonSerializer serializer = new JsonSerializer();
                // rooms = (ObservableCollection<Room>)serializer.Deserialize(file, typeof(ObservableCollection<Room>));
                apps = JsonConvert.DeserializeObject<ObservableCollection<Appointment>>(sr.ReadToEnd());
            }

            return apps;
        }

        public void Save(Appointment parameter1)
        {
            //  apps.Add(parameter1);

            apps.Add(parameter1);

            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, apps);
            }
        }

            public Boolean Delete(String id)
      {
            foreach (Appointment app1 in apps)
            {
                if (app1.IDAppointment == id)
                {
                    apps.Remove(app1);
                    using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, apps);
                    }
                    return true;
                }
            }
            return false;
        }
      
      public Appointment GetOne(String id)
      {
            foreach (Appointment app1 in apps)
            {
                if (app1.IDAppointment == id)
                {
                    return app1;
                }
            }
            return null;
        }
      
      public ObservableCollection<Appointment> GetByPatient(String id)
      {
            ObservableCollection<Appointment> apps = GetAll();
            ObservableCollection<Appointment> patientApps=new ObservableCollection<Appointment>();
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

   

        public String fileName;
        public static ObservableCollection<Appointment> apps = new ObservableCollection<Appointment>();
    }
}