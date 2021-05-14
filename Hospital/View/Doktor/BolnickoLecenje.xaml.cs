using Hospital.Model;
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

namespace Hospital.View.Doktor
{
    
    public partial class BolnickoLecenje : Page,INotifyPropertyChanged
    {
        private RoomStorage roomStorage = new RoomStorage();
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
        public BolnickoLecenje(string patientId)
        {
            InitializeComponent();
            this.DataContext = this;
            initProperties(patientId);
        }

        private void initProperties(string patientId)
        {
            medicalRecord=medicalRecordStorage.GetByPatientID(patientId);
            foreach(Room roomForDisplay in roomStorage.GetAll())
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
            ((Doctor_Examination)Window.GetWindow(this)).tab.SelectedIndex = 1;
            ((Doctor_Examination)Window.GetWindow(this)).hospitalTreatmentTab.IsEnabled = false;
            ((Doctor_Examination)Window.GetWindow(this)).TreatmentLabel.Foreground = Brushes.Black;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            changeUI();
        }
    }
}
