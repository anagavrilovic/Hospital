using Hospital.Model;
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

            if (appointment == null) 
                appointment = new ObservableCollection<Appointment>();

            appointment.Add(parameter1);
            DoSerialization(appointment);
        }

        public bool SaveIfNotOvelapping(Appointment period)
        {
            ObservableCollection<Appointment> appointments = GetAll();

            foreach (Appointment existingPeriod in appointments)
            {
                if(existingPeriod.IDDoctor.Equals(period.IDDoctor))
                    if (existingPeriod.DateTime < period.DateTime.AddHours(period.DurationInHours) && period.DateTime < existingPeriod.DateTime.AddHours(existingPeriod.DurationInHours))
                        return false;
            }

            appointments.Add(period);
            DoSerialization(appointments);
            return true;
        }

        public bool CheckIfOverlap(DateTime startTime, DateTime endTime, string doctorID, ObservableCollection<MoveAppointment> option)
        {
            ObservableCollection<Appointment> appointments = GetAll();

            foreach(Appointment app in appointments)
            {
                bool exisits = false;
                if (app.IDDoctor.Equals(doctorID))
                    if (app.DateTime < endTime && startTime < app.DateTime.AddHours(app.DurationInHours))
                    {
                        foreach(var op in option)
                        {
                            if (op.Appointment.IDAppointment.Equals(app.IDAppointment))
                            {
                                exisits = true;
                                break;
                            }
                        }

                        if (exisits)
                            continue;

                        return true;
                    }
            }

            return false;
        }

        public ObservableCollection<Appointment> GetOverlappingAppointments(Appointment appointment)
        {
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            ObservableCollection<Appointment> allAppointmnets = GetAll();

            foreach (Appointment app in allAppointmnets)
            {
                if (app.IDDoctor.Equals(appointment.IDDoctor))
                    if (app.DateTime < appointment.DateTime.AddHours(appointment.DurationInHours) && appointment.DateTime < app.DateTime.AddHours(app.DurationInHours))
                    {
                        appointments.Add(app);
                    }
            }

            return appointments;
        }

        public Appointment GetByID(String id)
        {
            ObservableCollection<Appointment> appointments = GetAll();
            foreach(Appointment app in appointments)
            {
                if (app.IDAppointment.Equals(id))
                {
                    return app;
                }
            }

            return new Appointment();
        }

        public void RescheduleAppointments(ObservableCollection<MoveAppointment> option)
        {
            foreach (var moveAppointmnet in option)
            {
                RescheduleAppointment(moveAppointmnet.Appointment.IDAppointment, moveAppointmnet.ToTime);
            }
        }

        public void RescheduleAppointment(string idAppointment, DateTime newTime)
        {
            ObservableCollection<Appointment> allAppointments = GetAll();
            
            foreach(var appointment in allAppointments)
            {
                if (appointment.IDAppointment.Equals(idAppointment))
                {
                    appointment.DateTime = newTime;
                    break;
                }
            }

            DoSerialization(allAppointments);
        }

        public ObservableCollection<Appointment> GetByPatient(String id)
        {
            ObservableCollection<Appointment> apps = GetAll();
            ObservableCollection<Appointment> patientApps = new ObservableCollection<Appointment>();
            Boolean found = false;
            if (apps == null)
            {
                return null;
            }
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
            if (apps==null || apps.Count == 0)
            {
                return retVal.ToString();
            }
           

            List<int> lista = new List<int>();
            foreach (Appointment app in apps)
            {
                int x = Int32.Parse(app.IDAppointment);
                lista.Add(x);
            }

            while (true)
            {
                if (!lista.Contains(retVal))
                {
                    break;
                }
                else
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

        public Boolean ExistByTime(DateTime dt,String idDoctor)
        {
            ObservableCollection<Appointment> appointment = GetAll();
            if (appointment == null) return false;
            foreach (Appointment a in appointment)
            {
                if (a.DateTime == dt && a.IDDoctor.Equals(idDoctor))
                {
                    return true;
                }
            }

            return false;
        }

        public int GetNumberOfAppointmentsForDoctor(string id)
        {
            ObservableCollection<Appointment> appointment = GetAll();
            int number = 0;
            if (appointment == null) return 0;
            foreach (Appointment a in appointment)
            {
                if (a.IDDoctor.Equals(id))
                {
                    number++;
                }
            }

            return number;
        }


    }
}