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

        public List<Doctor> GetAll()
        {
            return doctorRepository.GetAll();
        }

        public Doctor GetByID(string doctorID)
        {
            return doctorRepository.GetByID(doctorID);
        }

        public List<Doctor> GetDoctorsBySpecialty(DoctorSpecialty doctorsSpecialty)
        {
            return doctorRepository.GetBySpecialty(doctorsSpecialty);
        }

        public List<string> GetDoctorsNameSurname()
        {
            List<string> doctorsNameSurname = new List<string>();
            foreach (Hospital.Model.Doctor doctor in GetAll())
                doctorsNameSurname.Add(doctor.ToString());

            return doctorsNameSurname;
        }

        public String GetIDByNameSurname(string nameSurname)
        {
            return doctorRepository.GetIDByNameSurname(nameSurname);
        }

        public String GetByUsername(string username)
        {
            List<Doctor> doctors = GetAll();
            foreach (Doctor d in doctors)
                if (d.Username.Equals(username))
                    return d.PersonalID;

            return null;
        }

        public string GetUsernameByIDDoctor(string iDDoctor)
        {
            List<Doctor> doctors = GetAll();
            foreach (Doctor d in doctors)
                if (d.PersonalID.Equals(iDDoctor))
                    return d.Username;

            return null;
        }
    }
}
