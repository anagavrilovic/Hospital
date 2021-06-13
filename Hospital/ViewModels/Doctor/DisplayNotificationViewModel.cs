using Hospital.Commands.DoctorCommands;
using Hospital.Controller;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModels.Doctor
{
   public class DisplayNotificationViewModel : ViewModel
    {
        public Action CloseAction { get; set; }
        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get { return backCommand; }
            set
            {
                backCommand = value;
            }
        }
        NavigationController navigationController;
        public Notification Notification { get; set; }
        public DisplayNotificationViewModel(Notification notification, NavigationController navigationController)
        {
            this.navigationController = navigationController;
            this.BackCommand = new RelayCommand(Execute_Back, CanExecute_Command);
            this.Notification = notification;
        }
        private void Execute_Back(object sender)
        {
            CloseAction();
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

    }
}
