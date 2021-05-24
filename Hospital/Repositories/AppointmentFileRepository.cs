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
    public class AppointmentFileRepository : IAppointmentRepository
    {
        private string fileName = "appointments.json";

        public AppointmentFileRepository() { }

        public void Delete(string appointmentID)
        {
            ObservableCollection<Appointment> appointments = GetAll();
            foreach (Appointment a in appointments)
            {
                if (a.IDAppointment.Equals(appointmentID))
                {
                    appointments.Remove(a);
                    Serialize(appointments);
                    return;
                }
            }
        }

        public void DeleteByPatientID(string patientID)
        {
            ObservableCollection<Appointment> appointments = GetAll();
            foreach (Appointment a in appointments.ToList())
                if (a.IDpatient.Equals(patientID))
                    appointments.Remove(a);

            Serialize(appointments);
        }

        public ObservableCollection<Appointment> GetAll()
        {
            ObservableCollection<Appointment> appointments;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                appointments = (ObservableCollection<Appointment>)serializer.Deserialize(file, typeof(ObservableCollection<Appointment>));
            }

            if (appointments == null)
                appointments = new ObservableCollection<Appointment>();

            return appointments;
        }

        public ObservableCollection<Appointment> GetByDoctorID(string doctorID)
        {
            ObservableCollection<Appointment> appointments = GetAll();
            ObservableCollection<Appointment> doctorsAppointments = new ObservableCollection<Appointment>();

            foreach(Appointment appointment in appointments)
                if (appointment.IDDoctor.Equals(doctorID))
                    doctorsAppointments.Add(appointment);

            return doctorsAppointments;
        }

        public Appointment GetByID(string appointmentID)
        {
            ObservableCollection<Appointment> appointments = GetAll();
            foreach (Appointment a in appointments)
                if (a.IDAppointment.Equals(appointmentID))
                    return a;

            return new Appointment();
        }

        public ObservableCollection<Appointment> GetByPatientID(string patientID)
        {
            ObservableCollection<Appointment> appointments = GetAll();
            ObservableCollection<Appointment> patientsAppointments = new ObservableCollection<Appointment>();

            foreach (Appointment appointment in appointments)
                if (appointment.IDpatient.Equals(patientID))
                    patientsAppointments.Add(appointment);

            return patientsAppointments;
        }

        public List<int> GetExistingIDs()
        {
            ObservableCollection<Appointment> appointments = GetAll();
            List<int> existingIDs = new List<int>();

            foreach (Appointment appointment in appointments)
                existingIDs.Add(Int32.Parse(appointment.IDAppointment));

            return existingIDs;
        }

        public void Save(Appointment appointment)
        {
            ObservableCollection<Appointment> appointments = GetAll();
            appointments.Add(appointment);
            Serialize(appointments);
        }

        public void Serialize(ObservableCollection<Appointment> appointments)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, appointments);
            }
        }
        public bool IsDoctorAvaliableForAppointment(Appointment newAppointment)
        {
            ObservableCollection<Appointment> allAppointments = GetAll();
            foreach (Appointment appointment in allAppointments)
            {
                if (!appointment.IsDoctorAvaliable(newAppointment))
                    return false;
            }

            return true;
        }

        public bool IsPatientAvaliableForAppointment(Appointment newAppointment)
        {
            ObservableCollection<Appointment> allAppointments = GetAll();
            foreach (Appointment appointment in allAppointments)
            {
                if (!appointment.IsPatientAvaliable(newAppointment))
                    return false;
            }

            return true;
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
            if (apps == null || apps.Count == 0)
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
    }
}
