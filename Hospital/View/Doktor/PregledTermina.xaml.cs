using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

    public partial class MakeApointment : Page, INotifyPropertyChanged
    {
        public ObservableCollection<Appointment> Appointments
        {
            get; set;
        }
        public double DurationInHours 
        { 
            get => _durationInHours;
            set 
            {
                _durationInHours = value; 
            }
        }
        private ObservableCollection<string> comboBoxItems;
        public ObservableCollection<string> ComboBoxItems
        {
            get => comboBoxItems;
            set
            {
                comboBoxItems = value;
            }
        }
        

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string patientId;
        public event PropertyChangedEventHandler PropertyChanged;
        AppointmentStorage aStorage;
        private double _durationInHours;
        private DoctorStorage dStorage = new DoctorStorage();

        public MakeApointment(Hospital.Model.Doctor d,string patientId)
        {
            Appointments = new ObservableCollection<Appointment>();
            InitializeComponent();
            this.DataContext = this;
            this.patientId = patientId;
            ComboBox.ItemsSource = Enum.GetValues(typeof(DoctorSpecialty)).Cast<DoctorSpecialty>();
            ComboBox.SelectedIndex =(int)d.Specialty ;
            dataGridPregledi.Loaded += setMinWidths;
        }
        

        private void setMinWidths(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGridPregledi.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            KreiranjeTermina termin = new KreiranjeTermina(this,patientId);
            dodaj.IsEnabled = false;
            termin.Owner = Window.GetWindow(this);
            termin.Show();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Appointments.Clear();
            aStorage = new AppointmentStorage();
            foreach (Appointment a in aStorage.GetAll())
            {
                Hospital.Model.Doctor d = dStorage.GetOne(a.IDDoctor);
                if (d.Specialty.Equals((DoctorSpecialty)ComboBox.SelectedItem))
                {
                    Appointments.Add(a);
                }
            }
        }
    }
}
