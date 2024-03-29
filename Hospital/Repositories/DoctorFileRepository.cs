﻿using Hospital.Model;
using Hospital.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hospital.Repositories
{
    public class DoctorFileRepository : IDoctorRepository
    {
        private string fileName = "doctors.json";

        public void Delete(string id)
        {
            List<Doctor> doctors = GetAll();
            foreach (Doctor d in doctors)
            {
                if (d.PersonalID.Equals(id))
                {
                    doctors.Remove(d);
                    Serialize(doctors);
                    return;
                }
            }
        }

        public List<Doctor> GetAll()
        {
            List<Doctor> doctors;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                doctors = (List<Doctor>)serializer.Deserialize(file, typeof(List<Doctor>));
            }

            if (doctors == null)
                doctors = new List<Doctor>();

            return doctors;
        }

        public Doctor GetByID(string id)
        {
            List<Doctor> doctors = GetAll();
            foreach (Doctor d in doctors)
                if (d.PersonalID.Equals(id))
                    return d;

            return null;
        }

        public List<Doctor> GetBySpecialty(DoctorSpecialty requestedSpecialty)
        {
            List<Doctor> allDoctors = GetAll();
            List<Doctor> doctorsWithRequestedSpecialty = new List<Doctor>();

            foreach (Doctor doctor in allDoctors)
                if (doctor.Specialty.Equals(requestedSpecialty))
                    doctorsWithRequestedSpecialty.Add(doctor);

            return doctorsWithRequestedSpecialty;
        }

        public void Save(Doctor doctor)
        {
            List<Doctor> doctors = GetAll();
            doctors.Add(doctor);
            Serialize(doctors);
        }

        public void Serialize(List<Doctor> doctors)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, doctors);
            }
        }

        public void Update(Doctor doctorForUpdating)
        {
            List<Doctor> doctors = GetAll();
            doctors[doctors.FindIndex(doctor => doctor.PersonalID.Equals(doctorForUpdating.PersonalID))] = doctorForUpdating;
            Serialize(new List<Doctor>(doctors));
        }

        public String GetIDByNameSurname(string nameSurname)
        {
            String[] nameSurnameSplitted = nameSurname.Split(' ');
            List<Doctor> doctors = GetAll();
            foreach (Doctor d in doctors)
            {
                if (d.FirstName.Equals(nameSurnameSplitted[1]) && d.LastName.Equals(nameSurnameSplitted[2]))
                {
                    return d.PersonalID;
                }
            }
            return null;
        }
    }
}
