using Hospital.Factory;
using Hospital.Model;
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
    public partial class IzmenaSmene : Page
    {
        public ObservableCollection<Model.Doctor> AllDoctors { get; set; }
        public Model.Doctor SelectedDoctor { get; set; }
        public ScheduledShift NewScheduledShift { get; set; }
        public string OldShift { get; set; }

        public DoctorService DoctorService { get; set; }
        public DoctorsShiftService DoctorsShiftService { get; set; }

        public IzmenaSmene()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeAllProperties();
            LoadAllDoctors();
        }
        private void InitializeAllProperties()
        {
            DoctorService = new DoctorService();
            DoctorsShiftService = new DoctorsShiftService(new DoctorFileFactory());
            NewScheduledShift = new ScheduledShift();
        }

        private void LoadAllDoctors()
        {
            AllDoctors = new ObservableCollection<Model.Doctor>(DoctorService.GetAll());
        }

        private void ButtonYes(object sender, RoutedEventArgs e)
        {
            if(DoctorComboBox.SelectedIndex == -1 || ShiftComboBox.SelectedIndex == -1)
            {
                InformationBox informationBox = new InformationBox("Popunite sva polja!");
                informationBox.Show();
                return;
            }

            DoctorsShiftService.ChangeShift(SelectedDoctor, NewScheduledShift);
            NavigationService.Navigate(new RadnoVreme());
        }

        private void ButtonCancel(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RadnoVreme());
        }

        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedDoctor != null)
            {
                OldShift = DoctorsShiftService.GetDoctorsShiftByDate(SelectedDoctor, (DateTime)DatePickerDate.SelectedDate);
                OldShiftText.Text = OldShift;
            }
        }
    }
}
