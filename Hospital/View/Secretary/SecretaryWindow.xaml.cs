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
            Main.Navigate(new PocetnaStranica());
        }

        private void NextBtnClick(object sender, RoutedEventArgs e)
        {
            if (Main.NavigationService.CanGoForward)
                Main.NavigationService.GoForward();
            else
                Main.Navigate(new PocetnaStranica());
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            if (Main.NavigationService.CanGoBack)
                Main.NavigationService.GoBack();
            else
                Main.Navigate(new PocetnaStranica());
        }

        private void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonPocetnaClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new PocetnaStranica());
        }

        private void ButtonPacijentiClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new PrikazPacijenata());
        }

        private void ButtonKalendarClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Kalendar(null));
        }

        private void ButtonHitanPregledClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new HitanPregled(new HitanPregledViewModel(Main.NavigationService)));
        }

        private void ButtonRadnoVremeClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new RadnoVreme());
        }

        private void ButtonObavestenjaClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new ObavestenjaSekretar());
        }

        private void ButtonAnalitikaClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Analitika(new AnalitikaViewModel(Main.NavigationService)));
        }

        private void ButtonNaseBolniceClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new NaseBolnice());
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
