using Hospital.Model;
using Hospital.View.Doktor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{

    public partial class Doctor_Examination : Window, INotifyPropertyChanged
    {
        private Examination pregled;

        private Hospital.Model.Doctor doktor;
        public Appointment appointment;
        private Frame frameKarton;
        private Frame frameAnamnesis;
        private Frame frameDiagnosis;
        private Frame frameAppointment;
        private Frame frameTherapy;
        private Frame frameHospitalTreatment;
        private KartonDoktorStranica kDS;
        private Anamnesis ana;
        private Diagnosis dia;
        private MakeApointment ma;
        private Terapija th;
        private BolnickoLecenje hospitalTreatment;
        public Hospital.Model.Doctor LoggedInDoctor
        {
            get { return doktor; }
            set
            {
                doktor = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private DoctorStorage dStorage = new DoctorStorage();
        public Examination Pregled
        {
            get { return pregled; }
            set
            {
                pregled = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public Doctor_Examination(Appointment a)
        {
            appointment = a;
            pregled = new Examination();
            LoggedInDoctor = new Hospital.Model.Doctor();
            LoggedInDoctor = dStorage.GetOne(a.IDDoctor);
            
            InitializeComponent();
            intiProperties();
        }

        private void intiProperties()
        {
            tab.SelectedIndex = 1;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            frameKarton = new Frame();
            MedicalRecordStorage mStorage = new MedicalRecordStorage();
            kDS = new KartonDoktorStranica((mStorage.GetByPatientID(appointment.IDpatient)).MedicalRecordID,appointment);
            frameKarton.Content = kDS;
            Karton.Content = frameKarton;
            frameAnamnesis = new Frame();
            ana = new Anamnesis();
            frameAnamnesis.Content = ana;
            frameAppointment = new Frame();
            ma = new MakeApointment(doktor, (mStorage.GetByPatientID(appointment.IDpatient)).Patient.PersonalID);
            frameAppointment.Content = ma;
            frameDiagnosis = new Frame();
            dia = new Diagnosis();
            frameDiagnosis.Content = dia;
            frameTherapy = new Frame();
            th = new Terapija();
            frameTherapy.Content = th;
            frameHospitalTreatment = new Frame();
            hospitalTreatment = new BolnickoLecenje(appointment.IDpatient);
            frameHospitalTreatment.Content = hospitalTreatment;
        }

        private void tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (tab.SelectedIndex)
            {
                case 1:
                    Karton.Content = frameKarton;
                    KartonLabela.Foreground = Brushes.White;
                    AnamnezaLabela.Foreground = Brushes.Black;
                    DiagnozaLabela.Foreground = Brushes.Black;
                    TerminiLabela.Foreground = Brushes.Black;
                    TerapijaLabela.Foreground = Brushes.Black;
                    TreatmentLabel.Foreground = Brushes.Black;
                    break;
                case 2:
                    Anamneza.Content = frameAnamnesis;
                    KartonLabela.Foreground = Brushes.Black;
                    AnamnezaLabela.Foreground = Brushes.White;
                    break;
                case 3:
                    Terapija.Content = frameTherapy;
                    KartonLabela.Foreground = Brushes.Black;
                    break;
                case 4:
                    Dijagnoza.Content = frameDiagnosis;
                    kDS.sacuvaj.Visibility = Visibility.Visible;
                    kDS.hospitalTreatmentButton.Visibility = Visibility.Visible;
                    KartonLabela.Foreground = Brushes.Black;
                    break;
                case 5:
                    Termini.Content = frameAppointment;
                    kDS.sacuvaj.Visibility = Visibility.Visible;
                    kDS.hospitalTreatmentButton.Visibility = Visibility.Visible;
                    KartonLabela.Foreground = Brushes.Black;
                    break;
                case 6:
                    hospitalTreatmentTab.Content = frameHospitalTreatment;
                    kDS.sacuvaj.Visibility = Visibility.Visible;
                    kDS.hospitalTreatmentButton.IsEnabled = false;
                    kDS.hospitalTreatmentButton.Visibility = Visibility.Visible;
                    KartonLabela.Foreground = Brushes.Black;
                    break;
            }
        }
    }
    }

