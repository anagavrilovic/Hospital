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
    }
}
