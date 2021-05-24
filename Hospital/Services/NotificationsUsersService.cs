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
    public class NotificationsUsersService
    {
        private INotificationsUsersRepository notificationUsersRepository;

        public NotificationsUsersService()
        {
            notificationUsersRepository = new NotificationsUsersFileRepository();
        }
        public List<NotificationsUsers> GetAll()
        {
            return notificationUsersRepository.GetAll();
        }
        public void DeleteNotificationsUsersByNotificationID(string id)
        {
            notificationUsersRepository.DeleteNotificationsUsersByNotificationID(id);
        }
    }
}
