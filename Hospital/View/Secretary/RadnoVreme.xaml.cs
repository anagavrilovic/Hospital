using Hospital.DTO;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    public partial class RadnoVreme : Page
    {
        public ObservableCollection<DoctorsShiftsDTO> DoctorsShifts { get; set; }
        public ICollectionView DoctorsShiftsCollection { get; set; }

        public DateTime SelectedDate { get; set; }
        public DatesForShiftsDTO DatesForShifts { get; set; }

        public DoctorsShiftService DoctorsShiftService { get; set; }

        public RadnoVreme()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeEmptyProperties();
            SetDatesForShowingShifts();
            LoadAllDoctorsShifts();
        }

        private void InitializeEmptyProperties()
        {
            SelectedDate = DateTime.Now;
            DatesForShifts = new DatesForShiftsDTO();
            DoctorsShiftService = new DoctorsShiftService();
        }

        private void LoadAllDoctorsShifts()
        {
            DoctorsShifts = new ObservableCollection<DoctorsShiftsDTO>(DoctorsShiftService.GetAllDoctorsShiftsForNextFiveDays(DateTime.Today));

            DoctorsShiftsCollection = CollectionViewSource.GetDefaultView(DoctorsShifts);
            DoctorsShiftsCollection.Filter = CustomFilterDoctors;
        }

        private void ChangeShiftClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new IzmenaSmene());
        }

        private void FreeDaysClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SlobodniDani());
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDatesForShowingShifts();
            DoctorsShifts = new ObservableCollection<DoctorsShiftsDTO>(DoctorsShiftService.GetAllDoctorsShiftsForNextFiveDays(SelectedDate));
            DoctorsTable.ItemsSource = DoctorsShifts;
        }

        private void SetDatesForShowingShifts()
        {
            int days = 0;
            PropertyInfo[] properties = typeof(DatesForShiftsDTO).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(DatesForShifts, SelectedDate.AddDays(days).ToString("dd.MM."));
                days += 1;
            }
        }

        private bool CustomFilterDoctors(object obj)
        {
            if (string.IsNullOrEmpty(DoctorsFilter.Text))
                return true;
            else
                return ((obj as DoctorsShiftsDTO).Doctor.ToString().IndexOf(DoctorsFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void DoctorFilterTextChanged(object sender, TextChangedEventArgs e)
      {
            CollectionViewSource.GetDefaultView(DoctorsTable.ItemsSource).Refresh();
        }
    }
}
