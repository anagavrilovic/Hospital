using Hospital.Commands.DoctorCommands;
using Hospital.Controller;
using Hospital.View.Doctor;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Hospital.ViewModels.Doctor
{
    public class DoctorMainPageViewModel : ViewModel
    {
        NavigationController navigationController;
        private string doctorId;
        private bool themeIsChecked;
        public bool ThemeIsChecked
        {
            get { return themeIsChecked; }
            set
            {
                themeIsChecked = value;
                OnPropertyChanged("ThemeIsChecked");
                ThemeChanged();
            }
        }
        private int langugeSelectedIndex;
        public int LangugeSelectedIndex
        {
            get { return langugeSelectedIndex; }
            set
            {
                langugeSelectedIndex = value;
                OnPropertyChanged("LangugeSelectedIndex");
                ComboBox_SelectionChanged();
            }
        }
        private RelayCommand appointmentsCommand;

        public RelayCommand AppointmentsCommand
        {
            get { return appointmentsCommand; }
            set
            {
                appointmentsCommand = value;
            }
        }
        private RelayCommand editMedicineCommand;

        public RelayCommand EditMedicineCommand
        {
            get { return editMedicineCommand; }
            set
            {
                editMedicineCommand = value;
            }
        }
        private RelayCommand hospitalizedCommand;
        public RelayCommand HospitalizedCommand
        {
            get { return hospitalizedCommand; }
            set
            {
                hospitalizedCommand = value;
            }
        }
        public DoctorMainPageViewModel(string doctorId, NavigationController navigationController)
        {
            HospitalizedCommand = new RelayCommand(Execute_Hospitalized, canExecuteMethod);
            EditMedicineCommand = new RelayCommand(Execute_EditMedicine, canExecuteMethod);
            AppointmentsCommand = new RelayCommand(Execute_Appointments, canExecuteMethod);
            LangugeSelectedIndex = 1;
            this.navigationController = navigationController;
            this.doctorId = doctorId;
            var app = (App)Application.Current;
            ThemeIsChecked = app.DarkTheme;
        }

        private bool canExecuteMethod(object parameter)
        {
            return true;
        }
        private void Execute_Appointments(object sender)
        {
            navigationController.NavigateToDoctorAppointments(doctorId);

        }

        private void Execute_EditMedicine(object sender)
        {
            navigationController.NavigateToDoctorEditMedics();
        }

        private void Execute_Hospitalized(object sender)
        {
            navigationController.NavigateToDoctorHospitalizedPatients();
        }
        private void ThemeChanged()
        {
            var app = (App)Application.Current;
            if (ThemeIsChecked.Equals(true))
                app.ChangeTheme(new Uri("Resources/DoctorResourceDictionaryDark.xaml", UriKind.Relative));
            else
                app.ChangeTheme(new Uri("Resources/DoctorResourceDictionary.xaml", UriKind.Relative));
            SetIcons(app);
        }

        private void SetIcons(App app)
        {
            Window parentWindow = GetThisWindow();
            if (!app.DarkTheme)
            {
                ((DoctorMainWindow)parentWindow).pills.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/pill-16.png", UriKind.Absolute));
                ((DoctorMainWindow)parentWindow).obavestenje.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/bell-2-16.png", UriKind.Absolute));
            }
            else
            {
                ((DoctorMainWindow)parentWindow).pills.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/pills.png", UriKind.Absolute));
                ((DoctorMainWindow)parentWindow).obavestenje.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/notification.png", UriKind.Absolute));
            }
        }

        private static Window GetThisWindow()
        {
            Window parentWindow = null;
            foreach (Window window in Application.Current.Windows)
            {
                if (window is DoctorMainWindow)
                {
                    parentWindow = window;
                    break;
                }
            }

            return parentWindow;
        }

        private void ComboBox_SelectionChanged()
        {
            var app = (App)Application.Current;
            if (LangugeSelectedIndex== 0)
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
