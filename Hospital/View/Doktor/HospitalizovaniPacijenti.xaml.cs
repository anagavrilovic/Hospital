using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class HospitalizovaniPacijenti : Page
    {
        private ObservableCollection<HospitalizedPatient> hospitalizedPatients = new ObservableCollection<HospitalizedPatient>();
        private HospitalTreatmentStorage hospitalTreatmentStorage = new HospitalTreatmentStorage();
        private RoomStorage roomStorage = new RoomStorage();
        private MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
        public ObservableCollection<HospitalizedPatient> HospitalizedPatients
        {
            get { return hospitalizedPatients; }
            set { hospitalizedPatients = value; }
        }

        public HospitalizovaniPacijenti()
        {
            InitializeComponent();
            this.DataContext = this;
            FillTable();
        }

        private void FillTable()
        {
            foreach (HospitalTreatment hospitalTreatment in hospitalTreatmentStorage.GetAll())
            {
                HospitalizedPatient hospitalizedPatient = new HospitalizedPatient();
                hospitalizedPatient.Patient=((medicalRecordStorage.GetByPatientID(hospitalTreatment.PatientId)).Patient);
                hospitalizedPatient.Room = (roomStorage.GetOne(hospitalTreatment.RoomId));
                hospitalizedPatient.EndOfTreatment = hospitalTreatment.EndOfTreatment;
                hospitalizedPatient.StartOfTreatment = hospitalTreatment.StartOfTreatment;
                hospitalizedPatients.Add(hospitalizedPatient);
            }
        }
    }
}
