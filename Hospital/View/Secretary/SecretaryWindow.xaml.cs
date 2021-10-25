using Hospital.View.Secretary;
using Hospital.ViewModels.Secretary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class SecretaryWindow : Window
    {
        public SecretaryWindow()
        {
            InitializeComponent();
            Main.Navigate(new Homepage(new HomepageViewModel(Main.NavigationService)));
        }

        private void NextBtnClick(object sender, RoutedEventArgs e)
        {
            if (Main.NavigationService.CanGoForward)
                Main.NavigationService.GoForward();
            else
                Main.Navigate(new Homepage(new HomepageViewModel(Main.NavigationService)));
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            if (Main.NavigationService.CanGoBack)
                Main.NavigationService.GoBack();
            else
                Main.Navigate(new Homepage(new HomepageViewModel(Main.NavigationService)));
        }

        private void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonPocetnaClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Homepage(new HomepageViewModel(Main.NavigationService)));
        }

        private void ButtonPacijentiClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new AllPatients());
        }

        private void ButtonKalendarClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Timetable(null));
        }

        private void ButtonHitanPregledClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new UrgentExamination(new UrgentExaminationViewModel(Main.NavigationService)));
        }

        private void ButtonRadnoVremeClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new WorkingHour());
        }

        private void ButtonObavestenjaClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new NotificationsSecretary());
        }

        private void ButtonAnalitikaClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Analytics(new AnalyticsViewModel(Main.NavigationService)));
        }

        private void ButtonNaseBolniceClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new OurHospitals());
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
