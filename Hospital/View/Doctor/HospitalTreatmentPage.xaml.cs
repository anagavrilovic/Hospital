using Hospital.DTO.DoctorDTO;
using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Hospital.View.Doctor
{
    
    public partial class HospitalTreatmentPage : Page,INotifyPropertyChanged
    {
        private static int MEDICAL_RECORD_TAB = 1;
        private RoomService roomService = new RoomService();
        private HospitalTreatment hospitalTreatment = new HospitalTreatment();
        public event PropertyChangedEventHandler PropertyChanged;
        private HospitalTreatmentDTO dTO;
        public HospitalTreatmentDTO DTO
        {
            get
            {
                return dTO;
            }
            set
            {
                if (value != dTO)
                {
                    dTO = value;
                    OnPropertyChanged("DTO");
                }
            }
        }
        private MedicalRecordService medicalRecordService = new MedicalRecordService();
        public HospitalTreatmentPage(string patientId)
        {
            InitializeComponent();
            this.DataContext = this;
            initProperties(patientId);
        }

        private void initProperties(string patientId)
        {
            DTO = new HospitalTreatmentDTO();
            DTO.MedicalRecord = medicalRecordService.GetByPatientId(patientId);
            foreach(Room roomForDisplay in roomService.GetAll())
            {
                if (roomForDisplay.Type.Equals(RoomType.SOBA_ZA_ODMOR))
                    DTO.RoomsForDisplay.Add(roomForDisplay);
            }
            roomComboBox.ItemsSource = DTO.RoomsForDisplay;
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            hospitalTreatment.PatientId = DTO.MedicalRecord.Patient.PersonalID;
            hospitalTreatment.RoomId = DTO.SelectedRoom.Id;
            hospitalTreatment.StartOfTreatment = DateTime.Now;
            hospitalTreatment.EndOfTreatment = DTO.PatientsStayDuration;
            HospitalTreatmentService hospitalTreatmentService = new HospitalTreatmentService();
            hospitalTreatmentService.Save(hospitalTreatment);
            changeUI();
        }

        private void changeUI()
        {
            ((AppointmentWindow)Window.GetWindow(this)).tab.SelectedIndex = MEDICAL_RECORD_TAB;
            ((AppointmentWindow)Window.GetWindow(this)).hospitalTreatmentTab.IsEnabled = false;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            changeUI();
        }
    }
}
