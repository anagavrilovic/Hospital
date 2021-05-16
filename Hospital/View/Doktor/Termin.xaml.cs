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
        private MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
        private Examination examination;
        private static int MEDICAL_RECORD_REVIEW_TAB = 1;
        private Model.Doctor loggedInDoctor;
        public Appointment appointment;
        private Frame frameMedicalRecordReview;
        private Frame frameAnamnesis;
        private Frame frameDiagnosis;
        private Frame frameAppointment;
        private Frame frameTherapy;
        private Frame frameHospitalTreatment;
        private KartonDoktorStranica medicalRecordReview;
        private Anamnesis anamnesis;
        private Diagnosis diagnosis;
        private MakeApointment makeAppointment;
        private Terapija therapy;
        private BolnickoLecenje hospitalTreatment;
        public Hospital.Model.Doctor LoggedInDoctor
        {
            get { return loggedInDoctor; }
            set
            {
                loggedInDoctor = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private DoctorStorage doctorStorage = new DoctorStorage();
        public Examination Examintaion
        {
            get { return examination; }
            set
            {
                examination = value;
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
        public Doctor_Examination(Appointment appointment)
        {
            this.appointment = appointment;
            examination = new Examination();
            LoggedInDoctor = new Hospital.Model.Doctor();
            LoggedInDoctor = doctorStorage.GetOne(appointment.IDDoctor);
            
            InitializeComponent();
            intiProperties();
        }

        private void intiProperties()
        {
            tab.SelectedIndex = MEDICAL_RECORD_REVIEW_TAB;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            SetFramesAndTheirContext();
        }

        private void SetFramesAndTheirContext()
        {
            SetMedicalRecordReview();
            SetAnamnesis();
            SetAppointment();
            SetDiagnosis();
            SetTherapy();
            SetHospitalTreatment();
        }

        private void SetHospitalTreatment()
        {
            frameHospitalTreatment = new Frame();
            hospitalTreatment = new BolnickoLecenje(appointment.IDpatient);
            frameHospitalTreatment.Content = hospitalTreatment;
        }

        private void SetTherapy()
        {
            frameTherapy = new Frame();
            therapy = new Terapija();
            frameTherapy.Content = therapy;
        }

        private void SetDiagnosis()
        {
            frameDiagnosis = new Frame();
            diagnosis = new Diagnosis();
            frameDiagnosis.Content = diagnosis;
        }

        private void SetAppointment()
        {
            frameAppointment = new Frame();
            makeAppointment = new MakeApointment(loggedInDoctor, (medicalRecordStorage.GetByPatientID(appointment.IDpatient)).Patient.PersonalID);
            frameAppointment.Content = makeAppointment;
        }

        private void SetAnamnesis()
        {
            frameAnamnesis = new Frame();
            anamnesis = new Anamnesis();
            frameAnamnesis.Content = anamnesis;
        }

        private void SetMedicalRecordReview()
        {
            frameMedicalRecordReview = new Frame();
            medicalRecordStorage = new MedicalRecordStorage();
            medicalRecordReview = new KartonDoktorStranica((medicalRecordStorage.GetByPatientID(appointment.IDpatient)).MedicalRecordID, appointment);
            frameMedicalRecordReview.Content = medicalRecordReview;
            medicalRecordReviewTab.Content = frameMedicalRecordReview;
        }

        private void tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (tab.SelectedIndex)
            {
                case 1:
                    medicalRecordReviewTab.Content = frameMedicalRecordReview;
                    medicalRecordReviewLabel.Foreground = Brushes.White;
                    AnamnesisLabel.Foreground = Brushes.Black;
                    DiagnosisLabel.Foreground = Brushes.Black;
                    AppointmentLabel.Foreground = Brushes.Black;
                    TherapyLabel.Foreground = Brushes.Black;
                    TreatmentLabel.Foreground = Brushes.Black;
                    break;
                case 2:
                    AnamnesisTab.Content = frameAnamnesis;
                    medicalRecordReviewLabel.Foreground = Brushes.Black;
                    AnamnesisLabel.Foreground = Brushes.White;
                    break;
                case 3:
                    TherapyTab.Content = frameTherapy;
                    medicalRecordReviewLabel.Foreground = Brushes.Black;
                    TherapyLabel.Foreground = Brushes.White;
                    break;
                case 4:
                    DiagnosisTab.Content = frameDiagnosis;
                    medicalRecordReview.saveButton.Visibility = Visibility.Visible;
                    if (!AlreadyHospitalized())
                    {
                        medicalRecordReview.hospitalTreatmentButton.Visibility = Visibility.Visible;
                    }
                    medicalRecordReviewLabel.Foreground = Brushes.Black;
                    DiagnosisLabel.Foreground = Brushes.White;
                    break;
                case 5:
                    AppointmentTab.Content = frameAppointment;
                    medicalRecordReview.saveButton.Visibility = Visibility.Visible;
                    if (!AlreadyHospitalized())
                    {
                        medicalRecordReview.hospitalTreatmentButton.Visibility = Visibility.Visible;
                    }
                    medicalRecordReviewLabel.Foreground = Brushes.Black;
                    AppointmentLabel.Foreground = Brushes.White;
                    break;
                case 6:
                    hospitalTreatmentTab.Content = frameHospitalTreatment;
                    medicalRecordReview.saveButton.Visibility = Visibility.Visible;
                    medicalRecordReview.hospitalTreatmentButton.IsEnabled = false;
                    medicalRecordReview.hospitalTreatmentButton.Visibility = Visibility.Visible;
                    medicalRecordReviewLabel.Foreground = Brushes.Black;
                    medicalRecordReviewLabel.Foreground = Brushes.White;
                    break;
            }
        }

        private bool AlreadyHospitalized()
        {
            HospitalTreatmentStorage hospitalTreatmentStorage = new HospitalTreatmentStorage();
            foreach(HospitalTreatment hospitalTreatment in hospitalTreatmentStorage.GetAll())
            {
                if (hospitalTreatment.PatientId.Equals(appointment.IDpatient))
                    return true;
            }
            return false;
        }
    }
    }

