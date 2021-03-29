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

    public partial class MakeApointment : Window, INotifyPropertyChanged
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
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        AppointmentStorage aStorage;
        private double _durationInHours;
        public MakeApointment()
        {
            InitializeComponent();
            this.DataContext = this;
            aStorage=new AppointmentStorage();
             Appointments = aStorage.GetAll();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment)dataGridPregledi.SelectedItem;
            Appointments.Remove((Appointment)dataGridPregledi.SelectedItem);
            aStorage.Delete(appointment.IDAppointment);
            dataGridPregledi.ItemsSource = aStorage.GetAll();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGridPregledi.SelectedItem != null)
            {
                IzmenaTermina termin = new IzmenaTermina((Appointment)dataGridPregledi.SelectedItem);
                termin.Owner = this;
                termin.Show();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            KreiranjeTermina termin = new KreiranjeTermina(this);
            termin.Owner=this;
            termin.Show();
        }
    }
}
