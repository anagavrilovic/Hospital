using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for DetaljiPregleda.xaml
    /// </summary>
    public partial class DetaljiPregleda : Window,INotifyPropertyChanged
    {
        Appointment appointment = new Appointment();

        public event PropertyChangedEventHandler PropertyChanged;

        public Appointment Appointment
        {
            get { return appointment; }
            set
            {
                appointment = value;
                OnPropertyChanged();
            }
        }
        private string trajanjeUMinutima;
        public string TrajanjeUMinutima
        {
            get { return trajanjeUMinutima; }
            set
            {
                trajanjeUMinutima = value;
                OnPropertyChanged();
            }
        }

        public DetaljiPregleda(Appointment a)
        {
            this.Appointment = a;
            InitializeComponent();
            InitFields();
            this.DataContext = this;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
        }

        private void InitFields()
        {
           double dur= Appointment.DurationInHours * 60;
            trajanjeUMinutima = dur.ToString() +" minuta";
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void pregledKartona(object sender, RoutedEventArgs e)
        {
            MedicalRecordStorage m = new MedicalRecordStorage();
            DoktorKarton d = new DoktorKarton((m.GetByPatientID(Appointment.IDpatient)).MedicalRecordID);
            d.Owner = this;
            this.Hide();
            d.Show();
        }

        private void zapocniTermin(object sender, RoutedEventArgs e)
        {
            Doctor_Examination d = new Doctor_Examination(appointment);
            d.Show();
            d.Owner=(Window.GetWindow(this) ).Owner;
            this.Close();

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
