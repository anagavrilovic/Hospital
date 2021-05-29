using Hospital.View;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
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
using Hospital.Model;
using System.Collections.ObjectModel;
using Hospital.View.Doctor;
using Hospital.Services;

namespace Hospital
{

    public partial class MainWindow : Window
    {
        public static String IDnumber;
        public DoctorsShiftService DoctorsShiftService { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DoctorsShiftService = new DoctorsShiftService();
            DoctorsShiftService.UpdateAllShifts();
        }

        private void BtnUlogujSe(object sender, RoutedEventArgs e)
        {
            RegistratedUserStorage rus = new RegistratedUserStorage();
            ObservableCollection<RegistratedUser> users = rus.GetAll();
            bool found = false;

            foreach (RegistratedUser user in users)
            {
                if (user.Username.Equals(Username.Text) && user.Password.Equals(Password.Password))
                {
                    found = true;
                    switch (user.Type)
                    {
                        case UserType.doctor:
                            DoctorStorage ds = new DoctorStorage();
                            IDnumber= ds.GetByUsername(user.Username);
                            DoctorMainWindow de = new DoctorMainWindow(IDnumber);
                            Application.Current.MainWindow=de;
                            de.Show();
                            this.Close();
                            break;
                        case UserType.manager:
                            MenagerWindow mw = new MenagerWindow();
                            mw.Owner = Application.Current.MainWindow;
                            mw.Show();
                            break;
                        case UserType.patient:
                            MedicalRecordStorage mds = new MedicalRecordStorage();
                            IDnumber = mds.GetByUsername(user.Username);
                            PatientMain patientMain = new PatientMain();
                            patientMain.Show();
                            this.Close();
                            break;
                        case UserType.secretary:
                            var s = new SecretaryWindow();
                            s.Show();
                            break;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("Nepostojeći korisnik!");
            }
        }

        private void BtnOtkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
