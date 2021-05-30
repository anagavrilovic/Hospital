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
        private Room selectedRoom = new Room();
        private MedicalRecord medicalRecord;
        public event PropertyChangedEventHandler PropertyChanged;
        private MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
        public Room SelectedRoom
        {
            get
            {
                return selectedRoom;
            }
            set
            {
                if (value != selectedRoom)
                {
                    selectedRoom = value;
                    OnPropertyChanged("selectedRoom");
                }
            }
        }
        public MedicalRecord MedicalRecord
        {
            get
            {
                return medicalRecord;
            }
            set
            {
                if (value != medicalRecord)
                {
                    medicalRecord = value;
                    OnPropertyChanged("medicalRecord");
                }
            }
        }
        private DateTime patientsStayDuration;
        public DateTime PatientsStayDuration
        {
            get
            {
                return patientsStayDuration;
            }
            set
            {
                if (value != patientsStayDuration)
                {
                    patientsStayDuration = value;
                    OnPropertyChanged("patientsStayDuration");
                }
            }
        }
        private ObservableCollection<Room> roomsForDisplay = new ObservableCollection<Room>();
        public ObservableCollection<Room> RoomsForDisplay
        {
            get
            {
                return roomsForDisplay;
            }
            set
            {
                if (value != roomsForDisplay)
                {
                    roomsForDisplay = value;
                    OnPropertyChanged("roomsForDisplay");
                }
            }
        }
        public HospitalTreatmentPage(string patientId)
        {
            InitializeComponent();
            this.DataContext = this;
            initProperties(patientId);
        }

        private void initProperties(string patientId)
        {
            medicalRecord=medicalRecordStorage.GetByPatientID(patientId);
            foreach(Room roomForDisplay in roomService.GetAll())
            {
                if (roomForDisplay.Type.Equals(RoomType.SOBA_ZA_ODMOR))
                    roomsForDisplay.Add(roomForDisplay);
            }
            roomComboBox.ItemsSource = RoomsForDisplay;
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
            hospitalTreatment.PatientId = medicalRecord.Patient.PersonalID;
            hospitalTreatment.RoomId = selectedRoom.Id;
            hospitalTreatment.StartOfTreatment = DateTime.Now;
            hospitalTreatment.EndOfTreatment = PatientsStayDuration;
            HospitalTreatmentStorage hospitalTreatmentStorage = new HospitalTreatmentStorage();
            hospitalTreatmentStorage.Save(hospitalTreatment);
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
