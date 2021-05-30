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
    public class AppointmentFileRepository : IAppointmentRepository
    {
        private string fileName = "appointments.json";

        private IDoctorRepository doctorRepository;
        private IMedicalRecordRepository medicalRecordRepository;

        public AppointmentFileRepository() {
            doctorRepository = new DoctorFileRepository();
            medicalRecordRepository = new MedicalRecordFileRepository();
        }

        public void Delete(string appointmentID)
        {
            List<Appointment> appointments = GetAll();
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

        public void DeletePatientsAppointments(string patientID)
        {
            List<Appointment> appointments = GetAll();
            foreach (Appointment a in appointments.ToList())
                if (a.IDpatient.Equals(patientID))
                    appointments.Remove(a);

            Serialize(appointments);
        }

        public List<Appointment> GetAll()
        {
            List<Appointment> appointments;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                appointments = (List<Appointment>)serializer.Deserialize(file, typeof(List<Appointment>));
            }

            if (appointments == null)
                appointments = new List<Appointment>();

            return appointments;
        }

        public List<Appointment> GetByDoctorID(string doctorID)
        {
            List<Appointment> appointments = GetAll();
            List<Appointment> doctorsAppointments = new List<Appointment>();

            foreach(Appointment appointment in appointments)
                if (appointment.IDDoctor.Equals(doctorID))
                    doctorsAppointments.Add(appointment);

            return doctorsAppointments;
        }

        public Appointment GetByID(string appointmentID)
        {
            List<Appointment> appointments = GetAll();
            foreach (Appointment a in appointments)
                if (a.IDAppointment.Equals(appointmentID))
                    return a;

            return new Appointment();
        }

        public List<Appointment> GetByPatientID(string patientID)
        {
            List<Appointment> appointments = GetAll();
            List<Appointment> patientsAppointments = new List<Appointment>();

            foreach (Appointment appointment in appointments)
                if (appointment.IDpatient.Equals(patientID))
                {
                    Appointment appointmentWithDoctor = SetDoctorForAppointment(appointment);
                    patientsAppointments.Add(appointmentWithDoctor);
                }

            return patientsAppointments;
        }

        public List<int> GetExistingIDs()
        {
            List<Appointment> appointments = GetAll();
            List<int> existingIDs = new List<int>();

            foreach (Appointment appointment in appointments)
                existingIDs.Add(Int32.Parse(appointment.IDAppointment));

            return existingIDs;
        }

        public void Save(Appointment appointment)
        {
            List<Appointment> appointments = GetAll();
            appointments.Add(appointment);
            Serialize(appointments);
        }

        public void Serialize(List<Appointment> appointments)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, appointments);
            }
        }
        public bool IsDoctorAvaliableForAppointment(Appointment newAppointment)
        {
            List<Appointment> allAppointments = GetAll();
            foreach (Appointment appointment in allAppointments)
            {
                if (!appointment.IsDoctorAvaliable(newAppointment))
                    return false;
            }

            return true;
        }

        public bool IsPatientAvaliableForAppointment(Appointment newAppointment)
        {
            List<Appointment> allAppointments = GetAll();
            foreach (Appointment appointment in allAppointments)
            {
                if (!appointment.IsPatientAvaliable(newAppointment))
                    return false;
            }

            return true;
        }
   
        public String GetNewID()
        {
            int newID = 0;
            while (true)
            {
                if (!CheckIfIDExists(newID.ToString())) return newID.ToString();
                newID++;
            }
        }

        private Boolean CheckIfIDExists(String ID)
        {
            List<Appointment> appointments = GetAll();
            appointments.AddRange(GetPassedAppointments());
            foreach (Appointment appointment in appointments)
            {
                if (appointment.IDAppointment.Equals(ID)) return true;
            }
            return false;
        }

        public Appointment SetDoctorForAppointment(Appointment appointment)
        {
           Doctor doctor=doctorRepository.GetByID(appointment.IDDoctor);
           appointment.Doctor = doctor;
           return appointment;
        }

        public List<Appointment> GetPassedAppointmentsForPatient(String id)
        {
            MedicalRecord medicalRecord = medicalRecordRepository.GetByPatientID(id);
            List<Appointment> appointments = new List<Appointment>();
            foreach (Examination examination in medicalRecord.Examination)
            {
                Appointment appointmentWithDoctor = SetDoctorForAppointment(examination.appointment);
                appointments.Add(appointmentWithDoctor);
            }
            return appointments;

        }

        public List<Appointment> GetPassedAppointments()
        {
            List<MedicalRecord> medicalRecords = medicalRecordRepository.GetAll();
            List<Appointment> appointments = new List<Appointment>();
            foreach (MedicalRecord medRec in medicalRecords)
            {
                foreach(Examination examination in medRec.Examination)
                {
                    appointments.Add(examination.appointment);
                }
            }
            return appointments;

        }

        public void Update(Appointment appointment)
        {
            List<Appointment> appointments = GetAll();
            appointments[appointments.FindIndex(a => a.IDAppointment.Equals(appointment.IDAppointment))] = appointment;
            Serialize(appointments);
        }
        
    }
}
