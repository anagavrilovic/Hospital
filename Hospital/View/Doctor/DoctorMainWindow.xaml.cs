using Hospital.Model;
using Hospital.Services.DoctorServices;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for DoktorGlavniProzor.xaml
    /// </summary>
    public partial class DoctorMainWindow : Window, INotifyPropertyChanged
    {
        private DoctorMainWindowService service = new DoctorMainWindowService();
        private Frame frameMainPage;
        private DoctorMainPage mainPage;
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

        public DoctorMainWindow(string doctorId)
        {
            InitializeComponent();
            this.DataContext = this;
            InitProperties(doctorId);
            service.checkHospitalTreatmentDates();

        }

        private void InitProperties(string doctorId)
        {
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            this.doctorId = doctorId;
            doctor = service.GetDoctorById(doctorId);
            frameMainPage = new Frame();
            mainPage = new DoctorMainPage(doctorId);
            frameMainPage.Content = mainPage;
            Main.Content = frameMainPage;
        }



        private void Logo(object sender, RoutedEventArgs e)
        {
            Main.Content = frameMainPage;
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
            Main.Content = new NotificationsPage(doctorId);
        }

        private void Revision_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new MedicineValidity(doctor));
        }


    }
}
