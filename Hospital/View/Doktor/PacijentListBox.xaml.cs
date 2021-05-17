using Hospital.View.Doktor;
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

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PacijentListBox.xaml
    /// </summary>
    public partial class PacijentListBox : Window, INotifyPropertyChanged
    {
        ObservableCollection<Patient> patients = new ObservableCollection<Patient>();
        MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
        public event PropertyChangedEventHandler PropertyChanged;
        private NoviTermin parentWindow;
        ObservableCollection<Patient> Patients
        {
            get { return patients; }
            set
            {
                patients = value;
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

        public PacijentListBox(NoviTermin parentWindow)
        {
            foreach (MedicalRecord record in medicalRecordStorage.GetAll())
            {
                Patients.Add(record.Patient);
            }
            InitializeComponent();
            this.DataContext = this;
            this.parentWindow = parentWindow;
            this.listBox.ItemsSource = Patients;
            SetSorterAndFilter();
        }

        private void SetSorterAndFilter()
        {
            ICollectionView view = GetPretraga();
            view.SortDescriptions.Add(new SortDescription("FirstName", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("LastName", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("PersonalID", ListSortDirection.Ascending));
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            parentWindow.Appointment.PatientsRecord = medicalRecordStorage.GetByPatientID(((Patient)listBox.SelectedItem).PersonalID);
            parentWindow.pacijentIme.Content = parentWindow.Appointment.PatientsRecord.Patient.FirstName + " " + parentWindow.Appointment.PatientsRecord.Patient.LastName;
            Close();
        }
        public ICollectionView GetPretraga()
        {
            return CollectionViewSource.GetDefaultView(patients);
        }

        private void filterPatients(object sender, RoutedEventArgs e)
        {
            ICollectionView view = GetPretraga();
            view.Filter = delegate (object item)
            {
                Patient p = item as Patient;
                string txt = p.FirstName + " " + p.LastName+" "+p.PersonalID;
                return txt.Contains(pretrazi.Text);
            };
        }
    }
}
