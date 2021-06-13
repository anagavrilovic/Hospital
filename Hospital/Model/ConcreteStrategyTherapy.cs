using Hospital.Factory;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class ConcreteStrategyTherapy : Strategy
    {
        private PatientTherapyNotificationService patientTherapyNotificationService = new PatientTherapyNotificationService();
        PatientTherapyMedicineNotification notification;
        public ConcreteStrategyTherapy(PatientTherapyMedicineNotification notification)
        {
            this.notification = notification;
        }

        public ConcreteStrategyTherapy()
        {

        }

        void Strategy.Update()
        {
            notification.LastRead = DateTime.Now;
            notification.Read = true;
            patientTherapyNotificationService.Update((PatientTherapyMedicineNotification)notification);

        }

        void Strategy.Delete()
        {
            patientTherapyNotificationService.Delete(notification.ID);
        }

        List<IPatientNotification> Strategy.GetNotifications()
        {
            List<PatientTherapyMedicineNotification> therapyNotifications = patientTherapyNotificationService.GetByPatientID();
            therapyNotifications = patientTherapyNotificationService.SetNotifactionsActivity(therapyNotifications);
            return new List<IPatientNotification>(therapyNotifications);
        }
    }
}
