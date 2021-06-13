using Hospital.DTO.DoctorDTO;
using Hospital.Factory;
using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for Terapija.xaml
    /// </summary>
    public partial class TherapyPage : Page, INotifyPropertyChanged
    {
        private MedicalRecordService medicalRecordService;
        public event PropertyChangedEventHandler PropertyChanged;
        private string therapyDescription;
        private TherapyDTO dTO;
        public TherapyDTO DTO
        {
            get { return dTO; }
            set
            {
                dTO = value;
                OnPropertyChanged("DTO");
            }
        }
        public string TherapyDescription
        {
            get { return therapyDescription; }
            set
            {
                therapyDescription = value;
                OnPropertyChanged("TherapyDescription");
            }
        }
        private string therapyTitle;
        public string TherapyTitle
        {
            get { return therapyTitle; }
            set
            {
                therapyTitle = value;
                OnPropertyChanged("TherapyTitle");
            }
        }

        public TherapyPage()
        {
            InitializeComponent();
            this.DataContext = this;
            DTO = new TherapyDTO();
            medicineBox.ItemsSource = DTO.MedicineView;
            medicalRecordService = new MedicalRecordService();
        }

        private void AddMedicine(object sender, RoutedEventArgs e)
        {
            MedicalRecord medicalRecord = medicalRecordService.GetByPatientId(((AppointmentWindow)Window.GetWindow(this)).appointment.IDpatient);
            MedicineTherapyWindow AddMedicineWindow = new MedicineTherapyWindow(this,medicalRecord);
            AddMedicineWindow.Owner = Window.GetWindow(this);
            AddMedicineWindow.ShowDialog();
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {

                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void SaveTherapyInExamination(object sender, RoutedEventArgs e)
        {
            Therapy therapy = new Therapy();
            FillTherapy(therapy);
            ((AppointmentWindow)Window.GetWindow(this)).Examintaion.therapy = therapy;
            SetTabColors();
        }

        private void SetTabColors()
        {
            ((AppointmentWindow)Window.GetWindow(this)).tab.SelectedIndex = 4;
            ((AppointmentWindow)Window.GetWindow(this)).TherapyTab.IsEnabled = false;
            ((AppointmentWindow)Window.GetWindow(this)).DiagnosisTab.IsEnabled = true;
        }

        private void FillTherapy(Therapy therapy)
        {
            foreach (MedicineTherapy medicineTherapyAdded in DTO.Medics)
            {
                therapy.AddMedicine(medicineTherapyAdded);
            }
            therapy.description = TherapyDescription;
            therapy.name = TherapyTitle;
        }

        private void RemoveMedicineFromList(object sender, RoutedEventArgs e)
        {
            if (medicineBox.SelectedItem != null)
            {
                DTO.Medics.Remove((MedicineTherapy)medicineBox.SelectedItem);
            }
        }
    }
}
