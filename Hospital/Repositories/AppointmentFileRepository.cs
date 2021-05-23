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

        public Appointment GetByID(string appointmentID)
        {
            ObservableCollection<Appointment> appointments = GetAll();
            foreach (Appointment a in appointments)
                if (a.IDAppointment.Equals(appointmentID))
                    return a;

            return new Appointment();
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
    }
}
