using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class AppointmentService
    {
        IAppointmentRepository appointmentRepository;
        public AppointmentService()
        {
            appointmentRepository = new AppointmentFileRepository();
        }
        public void DeleteAppointment(string id)
        {
            appointmentRepository.Delete(id);
        }

        public ObservableCollection<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }
        public void Save(Appointment appointment)
        {
            appointmentRepository.Save(appointment);
        }

        public bool IsDoctorAvaliableForAppointment(Appointment appointment)
        {
            return appointmentRepository.IsDoctorAvaliableForAppointment(appointment);
        }
        public bool IsPatientAvaliableForAppointment(Appointment appointment)
        {
            return appointmentRepository.IsPatientAvaliableForAppointment(appointment);
        }
        public String GetNewID()
        {
            return appointmentRepository.GetNewID();
        }
    }
}
