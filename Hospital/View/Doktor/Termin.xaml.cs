using Hospital.Model;
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
        private KartonDoktorStranica kDS;
        private Anamnesis ana;
        private Diagnosis dia;
        private MakeApointment ma;
        private Terapija th;
        public Hospital.Model.Doctor Doktor
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
            Doktor = new Hospital.Model.Doctor();
            Doktor = dStorage.GetOne(a.IDDoctor);
            
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
        }

        private void tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (tab.SelectedIndex)
            {
                case 1:
                    Karton.Content = frameKarton;
                    break;
                case 2:
                    Anamneza.Content = frameAnamnesis;
                    break;
                case 3:
                    Terapija.Content = frameTherapy;
                    break;
                case 4:
                    Dijagnoza.Content = frameDiagnosis;
                    break;
                case 5:
                    Termini.Content = frameAppointment;
                    break;
            }
        }
    }
    }

