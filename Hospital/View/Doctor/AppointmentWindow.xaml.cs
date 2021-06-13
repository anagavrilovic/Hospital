using Hospital.Factory;
using Hospital.Model;
using Hospital.Repositories;
using Hospital.Services;
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

namespace Hospital.View.Doctor
{

    public partial class AppointmentWindow : Window, INotifyPropertyChanged
    {
        private HospitalTreatmentService hospitalTreatmentService;
        private MedicalRecordService medicalRecordService;
        private DoctorService doctorService;
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
        private MedicalRecordPage medicalRecordReview;
        private Anamnesis anamnesis;
        private Diagnosis diagnosis;
        private AppointmentInfo makeAppointment;
        private TherapyPage therapy;
        private HospitalTreatmentPage hospitalTreatment;
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
        public AppointmentWindow(Appointment appointment)
        {
            hospitalTreatmentService = new HospitalTreatmentService();
            medicalRecordService = new MedicalRecordService();
            doctorService = new DoctorService(new DoctorFileRepository());
            this.appointment = appointment;
            examination = new Examination();
            LoggedInDoctor = new Hospital.Model.Doctor();
            LoggedInDoctor = doctorService.GetDoctorById(appointment.IDDoctor);           
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
            hospitalTreatment = new HospitalTreatmentPage(appointment.IDpatient);
            frameHospitalTreatment.Content = hospitalTreatment;
        }

        private void SetTherapy()
        {
            frameTherapy = new Frame();
            therapy = new TherapyPage();
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
            makeAppointment = new AppointmentInfo(loggedInDoctor, (medicalRecordService.GetByPatientId(appointment.IDpatient)).Patient.PersonalID);
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
            medicalRecordReview = new MedicalRecordPage((medicalRecordService.GetByPatientId(appointment.IDpatient)).MedicalRecordID, appointment);
            frameMedicalRecordReview.Content = medicalRecordReview;
            medicalRecordReviewTab.Content = frameMedicalRecordReview;
        }

        private void tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (tab.SelectedIndex)
            {
                case 1:
                    medicalRecordReviewTab.Content = frameMedicalRecordReview;
                    break;
                case 2:
                    AnamnesisTab.Content = frameAnamnesis;
                    break;
                case 3:
                    TherapyTab.Content = frameTherapy;
                    break;
                case 4:
                    DiagnosisTab.Content = frameDiagnosis;
                    medicalRecordReview.saveButton.Visibility = Visibility.Visible;
                    if (!hospitalTreatmentService.AlreadyHospitalized(appointment.IDpatient))
                    {
                        medicalRecordReview.hospitalTreatmentButton.Visibility = Visibility.Visible;
                    }
                    break;
                case 5:
                    AppointmentTab.Content = frameAppointment;
                    medicalRecordReview.saveButton.Visibility = Visibility.Visible;
                    if (!hospitalTreatmentService.AlreadyHospitalized(appointment.IDpatient))
                    {
                        medicalRecordReview.hospitalTreatmentButton.Visibility = Visibility.Visible;
                    }
                    break;
                case 6:
                    hospitalTreatmentTab.Content = frameHospitalTreatment;
                    medicalRecordReview.saveButton.Visibility = Visibility.Visible;
                    medicalRecordReview.hospitalTreatmentButton.IsEnabled = false;
                    medicalRecordReview.hospitalTreatmentButton.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
    }

