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
    public class PatientNotificationService
    {
        IPatientNotificationsRepository patientNotificationsRepository;

        public PatientNotificationService()
        {
            patientNotificationsRepository = new PatientNotificationsFileRepository();
        }
        public List<PatientTherapyMedicineNotification> GetByPatientID()
        {
            return patientNotificationsRepository.GetByPatientID();
        }

        public void Update(PatientTherapyMedicineNotification patientTherapyMedicineNotification)
        {
            patientNotificationsRepository.Update(patientTherapyMedicineNotification);
        }

        public void SaveFirst(Examination e)
        {
            MedicineStorage medicineStorage = new MedicineStorage();
            List<PatientTherapyMedicineNotification> patientNotifications = patientNotificationsRepository.GetAll();
            if (patientNotifications == null)
            {
                patientNotifications = new List<PatientTherapyMedicineNotification>();
            }
            int x = 0;
            foreach (MedicineTherapy med in e.therapy.Medicine)
            {
                x++;
                PatientTherapyMedicineNotification patientTherapyMedicineNotification = new PatientTherapyMedicineNotification();
                patientTherapyMedicineNotification.Name = e.therapy.name + ": " + medicineStorage.GetOne(med.MedicineID).Name;
                for (int i = 0; i < med.TimesPerDay; i++)
                {
                    TimeSpan ts = new TimeSpan(i * 3 + 10, 0, 0);
                    DateTime dateTime = DateTime.Now.Date + ts;
                    patientTherapyMedicineNotification.updateTimes(dateTime);
                }
                patientTherapyMedicineNotification.Read = false;
                DateTime date1 = e.appointment.DateTime;
                DateTime date2 = date1.AddDays(med.DurationInDays);
                patientTherapyMedicineNotification.FromDate = date1;
                patientTherapyMedicineNotification.ToDate = date2;
                patientTherapyMedicineNotification.updateDuration();
                if ((date1.Date <= DateTime.Now) && (date2.Date >= DateTime.Now))
                {
                    patientTherapyMedicineNotification.Active = true;
                }
                else
                {
                    patientTherapyMedicineNotification.Active = false;
                }
                patientTherapyMedicineNotification.ID = x.ToString();
                patientTherapyMedicineNotification.Description = e.therapy.description;
                patientTherapyMedicineNotification.IDpatient = e.appointment.IDpatient;
                patientNotifications.Add(patientTherapyMedicineNotification);
            }
            patientNotificationsRepository.Serialize(patientNotifications);

        }

        public List<PatientTherapyMedicineNotification> GetAll()
        {
            return patientNotificationsRepository.GetAll();
        }
    }
}
