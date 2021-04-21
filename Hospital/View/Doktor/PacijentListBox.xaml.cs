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
        ObservableCollection<Patient> pacijenti = new ObservableCollection<Patient>();
        MedicalRecordStorage mStorage = new MedicalRecordStorage();
        public event PropertyChangedEventHandler PropertyChanged;
        private KreiranjeTermina parentWindow;
        ObservableCollection<Patient> Pacijenti
        {
            get { return pacijenti; }
            set
            {
                pacijenti = value;
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

        public PacijentListBox(KreiranjeTermina parentWindow)
        {
            foreach (MedicalRecord record in mStorage.GetAll())
            {
                Pacijenti.Add(record.Patient);
            }
            InitializeComponent();
            this.DataContext = this;
            this.parentWindow = parentWindow;
            this.listBox.ItemsSource = Pacijenti;
            ICollectionView view = GetPretraga();
            view.SortDescriptions.Add(new SortDescription("FirstName", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("LastName", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("PersonalID", ListSortDirection.Ascending));
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            parentWindow.Pacijent= (Patient)listBox.SelectedItem;
            Close();
        }
        public ICollectionView GetPretraga()
        {
            return CollectionViewSource.GetDefaultView(pacijenti);
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
