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
    public class NotificationService
    {
        private INotificationRepository notificationRepository;

        public NotificationService()
        {
            notificationRepository = new NotificationFileRepository();
        }

        public ObservableCollection<Notification> GetAllNotifications()
        {
            return notificationRepository.GetAll();
        }
    }
}
