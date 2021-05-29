using Hospital.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Secretary
{
    public partial class SlobodniDani : Page
    {
        public ObservableCollection<Model.Doctor> AllDoctors { get; set; }
        public Model.Doctor SelectedDoctor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DoctorService DoctorService { get; set; }
        public DoctorsShiftService DoctorsShiftService { get; set; }


        public SlobodniDani()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeAllProperties();
            LoadAllDoctors();
        }

        private void InitializeAllProperties()
        {
            DoctorService = new DoctorService();
            DoctorsShiftService = new DoctorsShiftService();
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        private void LoadAllDoctors()
        {
            AllDoctors = new ObservableCollection<Model.Doctor>(DoctorService.GetAll());
        }

        private void PotvrdiClick(object sender, RoutedEventArgs e)
        {
            bool freeDaysSet = DoctorsShiftService.SetFreeDays(SelectedDoctor, StartDate, EndDate);
            if (!freeDaysSet)
            {
                int freeDaysLeft = DoctorsShiftService.GetFreeDays(SelectedDoctor);
                InformationBox informationBox = new InformationBox("Nema dovoljno slobodnihh dana. " + freeDaysLeft +
                    " slobodnih dana preostalo.");
                informationBox.Show();
                return;
            }

            NavigationService.Navigate(new RadnoVreme());
        }

        private void OdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RadnoVreme());
        }
    }
}
