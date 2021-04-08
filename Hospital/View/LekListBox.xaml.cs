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
    public partial class LekListBox : Window , INotifyPropertyChanged
    {
        ObservableCollection<Medicine> lekovi = new ObservableCollection<Medicine>();
        MedicineStorage mStorage = new MedicineStorage();
        public event PropertyChangedEventHandler PropertyChanged;
        private Terapija parentWindow;
        ObservableCollection<Medicine> Lekovi
        {
            get { return lekovi; }
            set
            {
                lekovi = value;
                OnPropertyChanged();
            }

        }
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

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public LekListBox(Terapija parentWindow)
        {
            InitializeComponent();
            foreach (Medicine medicine in mStorage.GetAll())
            {
                Boolean duplirani = false;
                foreach (Medicine medicine1 in parentWindow.Lekovi)
                {
                    if (medicine.ID.Equals(medicine1.ID))
                    {
                        duplirani = true;
                    }
                }
                if (!duplirani)
                {
                    Lekovi.Add(medicine);
                }
                
            }
            InitializeComponent();
            this.parentWindow = parentWindow;
            this.listBox.ItemsSource = Lekovi;
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
            parentWindow.Lekovi.Add((Medicine)listBox.SelectedItem);
            parentWindow.dani= Int32.Parse(Text);
            parentWindow.medicineBox.SelectedItem= ((Medicine)listBox.SelectedItem);
            Close();
        }
    }
}
