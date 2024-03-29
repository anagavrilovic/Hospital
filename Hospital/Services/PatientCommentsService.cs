﻿using Hospital.Factory;
using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class PatientCommentsService
    {
        IPatientCommentsRepository patientCommentsRepository;
        String _patientId;

        public PatientCommentsService(String patientId)
        {
            patientCommentsRepository = new PatientCommentsFileRepository();
            _patientId = patientId;
        }

        public void Save(PatientComment patientComment)
        {
            patientCommentsRepository.Save(patientComment);
        }

        public String GetNewID()
        {
            return patientCommentsRepository.GetNewID();
        }


        public Boolean IsAppointmentAlreadyGraded(String IDAppointment)
        {
            List<PatientComment> patientComments = patientCommentsRepository.GetAll();
            if (patientComments == null) return false;
            foreach (PatientComment a in patientComments)
            {
                if (a.IDAppointment.Equals(IDAppointment))
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean IsTimeForHospitalRating()
        {
            List<PatientComment> patientComments = patientCommentsRepository.GetAll();
            if (patientComments == null) return true;
            foreach (PatientComment a in patientComments)
            {
                if (a.IDPatient.Equals(_patientId) && String.IsNullOrEmpty(a.IDAppointment))
                {
                    if ((a.DateWhenRated - DateTime.Now).TotalDays < 150) return false;
                }
            }
            return true;

        }
    }
}
