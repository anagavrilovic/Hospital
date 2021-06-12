using Hospital.Commands.DoctorCommands;
using Hospital.Model;
using Hospital.Repositories;
using Hospital.Services;
using Hospital.View.Secretary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Secretary
{
    public class FeedbackViewModel : ViewModel
    {
        #region Properties

        public Feedback NewFeedback { get; set; }
        public FeedbackService FeedbackService { get; set; }
        public NavigationService NavigationService { get; set; }

        #endregion

        #region Konstruktor

        public FeedbackViewModel(NavigationService navigationService)
        {
            this.NavigationService = navigationService;
            SendFeedbackCommand = new RelayCommand(Execute_SendFeedbackCommand, CanExecuteCommands);
            CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecuteCommands);
            NewFeedback = new Feedback();
            FeedbackService = new FeedbackService(new FeedbackFileRepository());
        }

        #endregion

        #region Komande

        public RelayCommand SendFeedbackCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        #endregion

        #region Akcije

        public void Execute_SendFeedbackCommand(object obj)
        {
            NewFeedback.UserId = "5";
            FeedbackService.SaveFeedBack(NewFeedback);

            InformationBox informationBox = new InformationBox("Feedback poslat!");
            informationBox.Show();

            this.NavigationService.Navigate(new PocetnaStranica(new PocetnaStranicaViewModel(this.NavigationService)));
        }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        public void Execute_CancelCommand(object obj)
        {
            this.NavigationService.Navigate(new PocetnaStranica(new PocetnaStranicaViewModel(this.NavigationService)));
        }

        #endregion
    }
}
