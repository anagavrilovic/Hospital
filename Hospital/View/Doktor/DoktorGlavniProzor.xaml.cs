using Hospital.Model;
using Hospital.View.Doktor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for DoktorGlavniProzor.xaml
    /// </summary>
    public partial class DoktorGlavniProzor : Window, INotifyPropertyChanged
    {
    
        private string doctorId;
        private Model.Doctor doctor = new Model.Doctor();
        private DoctorStorage doctorStorage = new DoctorStorage();
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public DoktorGlavniProzor(string doctorId)
        { 
            InitializeComponent();
            this.DataContext = this;
            InitProperties(doctorId);
        }

        private void InitProperties(string doctorId)
        {
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            Main.Content =new DoktorGlavnaStranica(doctorId);
            this.doctorId = doctorId;
            doctor = doctorStorage.GetDoctorByID(doctorId);
            checkHospitalTreatmentDates();
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
        private void checkHospitalTreatmentDates()
        {
            HospitalTreatmentStorage hospitalTreatmentStorage = new HospitalTreatmentStorage();
            foreach(HospitalTreatment hospitalTreatment in hospitalTreatmentStorage.GetAll())
            {
                if (hospitalTreatment.EndOfTreatment.Date < DateTime.Today)
                    hospitalTreatmentStorage.DeleteByPatientId(hospitalTreatment.PatientId);
            }
        }

        private void Logo(object sender, RoutedEventArgs e)
        {
            Main.Content = new DoktorGlavnaStranica (doctorId);
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            this.Close();
        }

        private void Announcment_Click(object sender, RoutedEventArgs e)
        {
            //obavestenje.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/announcment.png", UriKind.Absolute));
            Main.Content = new DoktorObavestenja(doctorId);
        }

        private void Revision_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new ValidnostLeka(doctor));
        }
    }
}
