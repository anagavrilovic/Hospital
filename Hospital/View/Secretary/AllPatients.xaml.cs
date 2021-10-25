using Hospital.Factory;
using Hospital.Services;
using Hospital.View.Secretary;
using Hospital.ViewModels.Secretary;
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

namespace Hospital.View
{
    public partial class AllPatients : Page, INotifyPropertyChanged
    {
        public ObservableCollection<MedicalRecord> PatientsRecords { get; set; }
        public ICollectionView PatientCollection { get; set; }

        private MedicalRecord selectedPatientsRecord;
        public MedicalRecord SelectedPatientsRecord
        {
            get { return selectedPatientsRecord; }
            set { selectedPatientsRecord = value; OnPropertyChanged("SelectedPatientsRecord"); }
        }

        public MedicalRecordService MedicalRecordService { get; set; }

        public AllPatients()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeAllProperties();
            InitializePatientsRecords();
        }

        private void InitializeAllProperties()
        {
            this.PatientsRecords = new ObservableCollection<MedicalRecord>();
            this.MedicalRecordService = new MedicalRecordService();
            this.SelectedPatientsRecord = new MedicalRecord();
        }

        private void InitializePatientsRecords()
        {
            PatientsRecords = new ObservableCollection<MedicalRecord>(MedicalRecordService.GetAllRecords());
            PatientCollection = CollectionViewSource.GetDefaultView(PatientsRecords);
            PatientCollection.Filter = CustomFilterPatientsRecords;
        }

        private void NewPatientClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateMedicalRecord(new CreateMedicalRecordViewModel(this.NavigationService)));
        }
        
        private void UpdatePatientClick(object sender, RoutedEventArgs e)
        {
            if (!IsPatientSelected(PatientForUpdateNotSelectedMessage()))
                return;

            NavigationService.Navigate(new UpdateMedicalRecord(new UpdateMedicalRecordViewModel(this.NavigationService, SelectedPatientsRecord.MedicalRecordID)));
        }

        private void PatientsDetailsClick(object sender, RoutedEventArgs e)
        {
            if (!IsPatientSelected(PatientForDetailsNotSelectedMessage()))
                return;

            NavigationService.Navigate(new MedicalRecordDetails(new MedicalRecordDetailsViewModel(this.NavigationService, SelectedPatientsRecord)));
        }

        private void DeletePatientClick(object sender, RoutedEventArgs e)
        {
            if (!IsPatientSelected(PatientForDeletingNotSelectedMessage()))
                return;

            ConfirmBox confirmBox = new ConfirmBox("da želite da izbrišete pacijenta?");
            if ((bool)confirmBox.ShowDialog())
            {
                MedicalRecordService.DeletePatientsRecordFromSystem(SelectedPatientsRecord);
                PatientsRecords.Remove(SelectedPatientsRecord);
            }          
        }

        private void AlergeniClick(object sender, RoutedEventArgs e)
        {
            if (!IsPatientSelected(PatientForViewingAllergensNotSelectedMessage()))
                return;

            NavigationService.Navigate(new UpdateAllergens(SelectedPatientsRecord));
        }

        private void IsBlockedUnchecked(object sender, RoutedEventArgs e)
        {
            MedicalRecordService.UpdateAllRecords(PatientsRecords.ToList());
        }

        private void IsBlockedChecked(object sender, RoutedEventArgs e)
        {
            MedicalRecordService.UpdateAllRecords(PatientsRecords.ToList());
        }

        private bool IsPatientSelected(string message)
        {
            if (PacijentiTable.SelectedItem == null)
            {
                InformationBox informationBox = new InformationBox(message);
                informationBox.ShowDialog();
                return false;
            }

            return true;
        }

        private string PatientForUpdateNotSelectedMessage()
        {
            return "Selektujte pacijenta kojeg želite da izmenite!";
        }

        private string PatientForDetailsNotSelectedMessage()
        {
            return "Selektujte pacijenta čije informacije želite da pregledate!";
        }

        private string PatientForDeletingNotSelectedMessage()
        {
            return "Selektujte pacijenta kojeg želite da izbrišete!";
        }

        private string PatientForViewingAllergensNotSelectedMessage()
        {
            return "Selektujte pacijenta čije alergene želite da izmenite!";
        }

        private bool CustomFilterPatientsRecords(object obj)
        {
            if (string.IsNullOrEmpty(PatientsFilter.Text))
                return true;
            else
                return ((obj as MedicalRecord).Patient.FirstName.IndexOf(PatientsFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       ((obj as MedicalRecord).Patient.LastName.IndexOf(PatientsFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       ((obj as MedicalRecord).Patient.DateOfBirth.ToString("dd.MM.yyyy, HH:mm").IndexOf(PatientsFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       ((obj as MedicalRecord).Patient.PersonalID.IndexOf(PatientsFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       ("blokirani".IndexOf(PatientsFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 && ((obj as MedicalRecord).Patient.IsBlocked));
        }

        private void PatientsRecordsFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(PacijentiTable.ItemsSource).Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
