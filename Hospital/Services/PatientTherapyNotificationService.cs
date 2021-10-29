using Hospital.Factory;
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
    public class PatientTherapyNotificationService
    {
        IPatientTherapyNotificationRepository patientTherapyNotificationsRepository;
        IMedicineRepository medicineRepository;
        String _patientId;

        public PatientTherapyNotificationService(String patientId)
        {
            patientTherapyNotificationsRepository = new PatientTherapyNotificationFileRepository();
            medicineRepository = new MedicineFileRepository();
            _patientId = patientId;
        }
        public List<PatientTherapyMedicineNotification> GetByPatientID()
        {
            return patientTherapyNotificationsRepository.GetByPatientID(_patientId);
        }

        public void Update(PatientTherapyMedicineNotification patientTherapyMedicineNotification)
        {
            patientTherapyNotificationsRepository.Update(patientTherapyMedicineNotification);
        }

        public void SaveFirst(Examination e)
        {
            List<PatientTherapyMedicineNotification> patientNotifications =GetAll();
            foreach (MedicineTherapy med in e.therapy.Medicine)
            {
                patientNotifications.Add(GetNotification(e,med));
            }
            patientTherapyNotificationsRepository.Serialize(patientNotifications);

        }

        private PatientTherapyMedicineNotification GetNotification(Examination examination,MedicineTherapy medicineTherapy)
        {
            PatientTherapyMedicineNotification patientTherapyMedicineNotification = new PatientTherapyMedicineNotification(patientTherapyNotificationsRepository.GetNewID(), examination.appointment.IDpatient,false, examination.appointment.DateTime, examination.appointment.DateTime.AddDays(medicineTherapy.DurationInDays), medicineTherapy.Description);
            patientTherapyMedicineNotification.Name = medicineRepository.GetByID(medicineTherapy.MedicineID).Name;
            patientTherapyMedicineNotification = SetNotificationTimes(patientTherapyMedicineNotification, medicineTherapy);
            patientTherapyMedicineNotification.updateDuration();
            patientTherapyMedicineNotification = SetNotifactionActivity(patientTherapyMedicineNotification);
            return patientTherapyMedicineNotification;
        }

        private PatientTherapyMedicineNotification SetNotificationTimes(PatientTherapyMedicineNotification patientTherapyMedicineNotification,MedicineTherapy medicineTherapy)
        {
            for (int i = 0; i < medicineTherapy.TimesPerDay; i++)
            {
                TimeSpan ts = new TimeSpan(i * 3 + 10, 0, 0);
                DateTime dateTime = DateTime.Now.Date + ts;
                patientTherapyMedicineNotification.updateTimes(dateTime);
            }
            return patientTherapyMedicineNotification;
        }

        private PatientTherapyMedicineNotification SetNotifactionActivity(PatientTherapyMedicineNotification patientTherapyMedicineNotification)
        {
            if ((patientTherapyMedicineNotification.FromDate.Date <= DateTime.Now) && (patientTherapyMedicineNotification.ToDate >= DateTime.Now))
            {
                patientTherapyMedicineNotification.Active = true;
            }
            else
            {
                patientTherapyMedicineNotification.Active = false;
            }
            return patientTherapyMedicineNotification;
        }

        public List<PatientTherapyMedicineNotification> GetAll()
        {
            return patientTherapyNotificationsRepository.GetAll();
        }

        public Boolean IsThereNewNotification()
        {
            Boolean retVal = false;
            List<PatientTherapyMedicineNotification> notifications = GetByPatientID();
                foreach (PatientTherapyMedicineNotification notification in notifications)
                {
                    if ((notification.FromDate.Date <= DateTime.Now.Date) && (notification.ToDate.Date >= DateTime.Now.Date))
                    {
                        string[] times = notification.Times.Split(' ');
                        retVal = IsTimeForNotification(times,notification);
                        
                    }
                }
            return retVal;
        }

        private Boolean IsTimeForNotification(string[] notificationTimes,PatientTherapyMedicineNotification notification)
        {
            Boolean retVal = false;
            for (int i = 0; i < notificationTimes.Length - 1; i += 2)
            {
                DateTime dateTime = GetNotificationsDateTime(notificationTimes[i], notificationTimes[i+1]);
                if ((notification.LastRead < dateTime) && (dateTime <= DateTime.Now))
                {
                    retVal = true;
                    notification.Read = false;
                    Update(notification);
                }
            }
            return retVal;
        }

        private DateTime GetNotificationsDateTime(String time,String timePeriod)
        {
            TimeSpan ts = TimeSpan.Parse(time);
            DateTime dateTime = DateTime.Now.Date + ts;
            if (timePeriod.Equals("PM")) dateTime = dateTime.AddHours(12);
            return dateTime;
        }

        public List<PatientTherapyMedicineNotification> SetNotifactionsActivity(List<PatientTherapyMedicineNotification> patientTherapyMedicineNotifications)
        {
            foreach (PatientTherapyMedicineNotification patientTherapyMedicineNotification in patientTherapyMedicineNotifications)
            {
                if ((patientTherapyMedicineNotification.FromDate.Date <= DateTime.Now) && (patientTherapyMedicineNotification.ToDate >= DateTime.Now))
                {
                    patientTherapyMedicineNotification.Active = true;
                }
                else
                {
                    patientTherapyMedicineNotification.Active = false;
                }

            }
            return patientTherapyMedicineNotifications;
        }

        public void Delete(string id)
        {
            patientTherapyNotificationsRepository.Delete(id);
        }
    }
}
