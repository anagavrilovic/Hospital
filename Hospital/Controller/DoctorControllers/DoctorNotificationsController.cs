using Hospital.DTO.DoctorDTO;
using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Controller.DoctorControllers
{
    public class DoctorNotificationsController
    {
        private DoctorService doctorService;
        private NotificationService notificationService;
        private DoctorNotificationsDTO DTO;

        public DoctorNotificationsController(DoctorNotificationsDTO DTO)
        {
            this.DTO = DTO;
            doctorService = new DoctorService();
            notificationService = new NotificationService();
        }

        public Model.Doctor GetDoctorById(string doctorId)
        {
           return doctorService.GetDoctorById(doctorId);
        }

        public List<Notification> SetNotificationsProperty(Model.Doctor doctor)
        {
            return notificationService.SetNotificationsProperty(doctor);
        }

        public void DeleteNotificationsUsersByNotificationID()
        {
            notificationService.DeleteNotificationsUsersByNotificationID((DTO.SelectedNotification).Id);
        }
        public void DeleteNotification()
        {
            notificationService.DeleteNotification(DTO.SelectedNotification);
        }
    }
}
