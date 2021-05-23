﻿using Hospital.View.Doctor;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for DoktorGlavnaStranica.xaml
    /// </summary>
    public partial class DoctorMainPage : Page
    {
        private string doctorId;
        public DoctorMainPage(string doctorId)
        {
            InitializeComponent();
           this.doctorId = doctorId;
           var app = (App)Application.Current;
            themeCheckBox.IsChecked = app.DarkTheme;
            languageComboBox.SelectedIndex = 1;
        }
        private void Pregled(object sender, RoutedEventArgs e)
        {
            ((DoctorMainWindow)Window.GetWindow(this)).Main.Content = new DoctorAppointments(doctorId);
            
          
        }

        private void Lekovi(object sender, RoutedEventArgs e)
        {
            ((DoctorMainWindow)Window.GetWindow(this)).Main.Content = new EditMedicinePage();
        }

        private void Hospitalized_Click(object sender, RoutedEventArgs e)
        {
            ((DoctorMainWindow)Window.GetWindow(this)).Main.Navigate(new HospitalizedPatients());
        }
        private void ThemeChanged(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.IsChecked.Equals(true))
                app.ChangeTheme(new Uri("Resources/DoctorResourceDictionaryDark.xaml", UriKind.Relative));
            else
                app.ChangeTheme(new Uri("Resources/DoctorResourceDictionary.xaml", UriKind.Relative));
            SetIcons(app);
        }

        private void SetIcons(App app)
        {
            if (app.DarkTheme)
            {
                ((DoctorMainWindow)Window.GetWindow(this)).pills.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/pill-16.png", UriKind.Absolute));
                ((DoctorMainWindow)Window.GetWindow(this)).obavestenje.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/bell-2-16.png", UriKind.Absolute));
            }
            else
            {
                ((DoctorMainWindow)Window.GetWindow(this)).pills.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/pills.png", UriKind.Absolute));
                ((DoctorMainWindow)Window.GetWindow(this)).obavestenje.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/notification.png", UriKind.Absolute));
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var app = (App)Application.Current;
            if (languageComboBox.SelectedIndex == 0)
            {
                app.ChangeLanguage("sr-LATN");
            }
            else
            {
                app.ChangeLanguage("en-US");
            }
        }
    }
}