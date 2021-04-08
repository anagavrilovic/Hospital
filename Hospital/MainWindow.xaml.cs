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

namespace Hospital
{

    public partial class MainWindow : Window
    {
        public static String IDnumber;
        private ObservableCollection<RegistratedUser> users;

        public ObservableCollection<RegistratedUser> Users
        {
            get => users;
            set
            {
                users = value;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnUlogujSe(object sender, RoutedEventArgs e)
        {
            RegistratedUserStorage rus = new RegistratedUserStorage();
            users = rus.GetAll();
            bool found = false;

            foreach (RegistratedUser user in users)
            {
                if (user.Username.Equals(Username.Text) && user.Password.Equals(Password.Password))
                {
                    found = true;
                    switch (user.Type)
                    {
                        case UserType.doctor:
                            Doctor_Examination de = new Doctor_Examination();
                            string text = "";
                            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\dijagnoza.json"))
                            {
                                JsonSerializer serializer = new JsonSerializer();
                                serializer.Serialize(file, text);
                            }
                            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\anamneza.json"))
                            {
                                JsonSerializer serializer = new JsonSerializer();
                                serializer.Serialize(file, text);
                            }
                            DoctorStorage ds = new DoctorStorage();
                            IDnumber= ds.GetByUsername(user.Username);
                            de.Owner = Application.Current.MainWindow;
                            de.Show();
                            this.Hide();
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
    }
}
