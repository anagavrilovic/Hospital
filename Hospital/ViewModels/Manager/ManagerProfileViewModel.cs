using Hospital.Commands.Manager;
using Hospital.Model;
using Hospital.View;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Manager
{
    public class ManagerProfileViewModel : ViewModel
    {
        #region Properties
        public NavigationService NavigationService { get; set; }
        #endregion

        #region Constructor

        public ManagerProfileViewModel(NavigationService navigation)
        {
            NavigationService = navigation;
            BackCommand = new RelayCommand(Execute_BackCommand, CanExecuteCommands);
            FeedbackCommand = new RelayCommand(ExecuteFeedbackCommand, CanExecuteCommands);
        }
        #endregion

        #region Commands

        public RelayCommand BackCommand { get; set; }
        public RelayCommand FeedbackCommand { get; set; }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        public void ExecuteFeedbackCommand(object obj)
        {
            NavigationService.Navigate(new View.Manager.Feedback(new FeedbackViewModel(NavigationService)));
        }

        public void Execute_BackCommand(object obj)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }

        #endregion
    }
}
