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

        public void Update(PatientNotesNotification patientNotesNotification)
        {
            patientNotesNotificationRepository.Update(patientNotesNotification);
        }

        public Boolean IsThereNewNotification()
        {
            Boolean retval = false;
            List<PatientNotesNotification>  patientNotesNotifications=patientNotesNotificationRepository.GetByPatientID();
            foreach(PatientNotesNotification patientNotesNotification in patientNotesNotifications)
            {
                if (IsTimeForNotification(patientNotesNotification))
                {
                    retval = true;
                    patientNotesNotification.Read = false;
                    Update(patientNotesNotification);
                }
            }
            return retval;
        }

        private Boolean IsTimeForNotification(PatientNotesNotification patientNotesNotification)
        {
           List<DateTime> times = GetNotificationTimes(patientNotesNotification);
            foreach(DateTime time in times)
            {
                if(time<DateTime.Now && patientNotesNotification.LastRead < time)
                {
                    return true;
                }
            }
            return false;
        }

        private List<DateTime> GetNotificationTimes(PatientNotesNotification patientNotesNotification)
        {
            List<DateTime> days = new List<DateTime>();
            DateTime Monday = SetDayToMonday(DateTime.Now);
            for(int i = 0; i < 7; i++)
            {
                if (patientNotesNotification.Days[i]) days.Add(Monday.AddDays(i).Date+patientNotesNotification.HoursMinutes);
            }
            return days;
        }

        private DateTime SetDayToMonday(DateTime day)
        {
            int auxiliaryDay = (int)day.DayOfWeek;
            DateTime MondayDate;
            switch (auxiliaryDay)
            {
                case 2:
                    MondayDate = day.AddDays(-1);
                    break;
                case 3:
                    MondayDate = day.AddDays(-2);
                    break;
                case 4:
                    MondayDate = day.AddDays(-3);
                    break;
                case 5:
                    MondayDate = day.AddDays(-4);
                    break;
                case 6:
                    MondayDate = day.AddDays(-5);
                    break;
                case 0:
                    MondayDate = day.AddDays(-6);
                    break;
                default:
                    MondayDate = day;
                    break;
            }
            return MondayDate;
        }



    }
}
