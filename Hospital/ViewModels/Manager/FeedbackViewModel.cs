using Hospital.Commands.Manager;
using Hospital.Services;
using Hospital.View.Manager;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Hospital.Repositories;

namespace Hospital.ViewModels.Manager
{
    public class FeedbackViewModel : ViewModel
    {
        #region Properties
        public Model.Feedback NewFeedback { get; set; }
        public FeedbackService FeedbackService { get; set; }
        NavigationService NavigationService { get; set; }
        #endregion

        #region Constructor

        public FeedbackViewModel(NavigationService navigation)
        {
            NewFeedback = new Model.Feedback();
            FeedbackService = new FeedbackService(new FeedbackFileRepository());
            NavigationService = navigation;
            LeaveFeedbackCommand = new RelayCommand(ExecuteLeaveFeedbackCommand, CanExecuteCommands);
            CancelCommand = new RelayCommand(ExecuteCancelCommand, CanExecuteCommands);
            BackCommand = new RelayCommand(ExecuteBackCommand, CanExecuteCommands);
        }
        #endregion

        #region Commands

        public RelayCommand LeaveFeedbackCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        public void ExecuteLeaveFeedbackCommand(object obj)
        {
            NewFeedback.UserId = "11";
            FeedbackService.SaveFeedBack(NewFeedback);

            MessageWindow message = new MessageWindow("Feedback poslat!");
            message.Show();

            NavigationService.Navigate(new ManagerProfil(new ManagerProfileViewModel(NavigationService)));
        }

        public void ExecuteBackCommand(object obj)
        {
            NavigationService.Navigate(new ManagerProfil(new ManagerProfileViewModel(NavigationService)));
        }

        public void ExecuteCancelCommand(object obj)
        {
            NavigationService.Navigate(new ManagerProfil(new ManagerProfileViewModel(NavigationService)));
        }

        #endregion
    }
}
