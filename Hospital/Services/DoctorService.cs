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
    public class DoctorService
    {
        private IDoctorRepository doctorRepository;

        public DoctorService()
        {
            doctorRepository = new DoctorFileRepository();
        }

        public Doctor GetDoctorById(string id)
        {
            return doctorRepository.GetByID(id);
        }

        public ObservableCollection<Doctor> GetAll()
        {
            return doctorRepository.GetAll();
        }

        public Doctor GetByID(string doctorID)
        {
            return doctorRepository.GetByID(doctorID);
        }

        public ObservableCollection<Doctor> GetDoctorsBySpecialty(DoctorSpecialty doctorsSpecialty)
        {
            return doctorRepository.GetBySpecialty(doctorsSpecialty);
        }
    }
}
