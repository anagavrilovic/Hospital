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
    /// Interaction logic for LekListBox.xaml
    /// </summary>
    public partial class LekListBox : Window, INotifyPropertyChanged
    {
        ObservableCollection<Medicine> lekovi = new ObservableCollection<Medicine>();
        private MedicalRecord medicalRecord;
        MedicineStorage mStorage = new MedicineStorage();
        public event PropertyChangedEventHandler PropertyChanged;
        private Terapija parentWindow;
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }

        }
        ObservableCollection<Medicine> Lekovi
        {
            get { return lekovi; }
            set
            {
                lekovi = value;
                OnPropertyChanged();
            }

        }
        private string dana;
        public string Dana
        {
            get { return dana; }
            set
            {
                dana = value;
                OnPropertyChanged();
            }
        }
        private string dan;
        public string Dan
        {
            get { return dan; }
            set
            {
                dan = value;
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
        public LekListBox(Terapija parentWindow,MedicalRecord medicalRecord)
        {
            InitializeComponent();
            this.DataContext = this;
            this.medicalRecord = medicalRecord;
            for(int i = 1; i < 10; i++)
            {
                dnevni.Items.Add(i.ToString());
            }
            for(int i = 1; i < 60; i++)
            {
                danaZaK.Items.Add(i.ToString());
            }
            dnevni.SelectedIndex = 2;
            danaZaK.SelectedIndex = 6;
            Lekovi=mStorage.GetAll();
            this.parentWindow = parentWindow;
            this.listBox.ItemsSource = Lekovi;
            ICollectionView view = GetPretraga();
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
        }

        private void filterMedicine(object sender, RoutedEventArgs e)
        {
            ICollectionView view = GetPretraga();
            view.Filter = delegate (object item)
            {
                Medicine m = item as Medicine;
                string txt = m.Name + " " + m.ID;
                return txt.Contains(pretrazi.Text);
            };
        }
        public ICollectionView GetPretraga()
        {
            return CollectionViewSource.GetDefaultView(Lekovi);
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dan != null && dana != null)
            {
                Medicine m=((Medicine)listBox.SelectedItem);
                m.DurationInDays = int.Parse(Dana);
                m.TimesPerDay = int.Parse(Dan);
                m.Description = Text;
                MessageBox.Show(Text);
                bool postoji = false;
                foreach (Medicine m1 in parentWindow.Lekovi){
                    if (m1.ID.Equals(m.ID))
                    {
                        postoji = true;
                    }
                }
                if (postoji)
                {
                    MessageBox.Show("Vec ste dodali ovaj lek");
                }
                else
                {
                    foreach(string medicineName in medicalRecord.Allergen.MedicineNames)
                    {
                        if (medicineName.Equals(m.Name))
                        {
                            MessageBox.Show("Pacijent je alergican na dati lek");
                        }
                        else
                        {
                            parentWindow.Lekovi.Add(m);
                            this.Close();
                        }
                    }
                }
            }
        }

        private void dnevni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            dan = (string)dnevni.SelectedItem;
          
        }

        private void danaZaK_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dana = (string)danaZaK.SelectedItem;
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
