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
    class PatientNotesNotificationService
    {
        IPatientNotesNotificationRepository patientNotesNotificationRepository;

        public PatientNotesNotificationService()
        {
            patientNotesNotificationRepository = new PatientNotesNotificationFileRepository();
        }
        public void Delete(string id)
        {
            patientNotesNotificationRepository.Delete(id);
        }

        public List<PatientNotesNotification> GetAll()
        {
            return patientNotesNotificationRepository.GetAll();
        }

        public PatientNotesNotification GetByID(string id)
        {
            return patientNotesNotificationRepository.GetByID(id);
        }

        public void Save(PatientNotesNotification patientNotesNotification)
        {
            patientNotesNotificationRepository.Save(patientNotesNotification);
        }

        public String GetNewID()
        {
            return patientNotesNotificationRepository.GetNewID();
        }

        public List<PatientNotesNotification> GetByPatientID()
        {
            return patientNotesNotificationRepository.GetByPatientID();
        }
    }
}
