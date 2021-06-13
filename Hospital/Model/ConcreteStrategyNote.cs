using Hospital.Factory;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class ConcreteStrategyNote : Strategy
    {
        PatientNotesNotification notification;
        private PatientNotesNotificationService patientNotesNotificationService = new PatientNotesNotificationService(new PatientNotesNotificationFileFactory());

        public ConcreteStrategyNote(PatientNotesNotification notification)
        {
            this.notification = notification;
        }

        public ConcreteStrategyNote()
        {

        }
        void Strategy.Update()
        {
            notification.LastRead = DateTime.Now;
            notification.Read = true;
            patientNotesNotificationService.Update((PatientNotesNotification)notification);
        }

        void Strategy.Delete()
        {
            patientNotesNotificationService.Delete(notification.ID);
        }

        List<IPatientNotification> Strategy.GetNotifications()
        {
            return new List<IPatientNotification>(patientNotesNotificationService.GetByPatientID()); ;
        }
    }
}
