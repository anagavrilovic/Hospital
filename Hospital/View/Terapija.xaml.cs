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
    /// Interaction logic for Terapija.xaml
    /// </summary>
    public partial class Terapija : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Medicine> lekovi = new ObservableCollection<Medicine>();
        public int dani;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Medicine> Lekovi
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
        private string naslov;
        public string Naslov
        {
            get { return naslov; }
            set
            {
                naslov = value;
                OnPropertyChanged();
            }
        }

        public Terapija()
        {
            InitializeComponent();
            this.DataContext = this;
            medicineBox.ItemsSource = Lekovi;
        }

        private void DodajLek(object sender, RoutedEventArgs e)
        {
            LekListBox lb = new LekListBox(this);
            lb.Owner = Window.GetWindow(this);
            lb.ShowDialog();
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {

                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Sacuvaj(object sender, RoutedEventArgs e)
        {
            Therapy t = new Therapy();
            foreach (Medicine lek in Lekovi)
            {
                t.AddMedicine(lek);
            }
            t.description = Text;
            t.name = Naslov;
            ((Doctor_Examination)Window.GetWindow(this)).Pregled.therapy = t;
            ((Doctor_Examination)Window.GetWindow(this)).tab.SelectedIndex = 4;
            ((Doctor_Examination)Window.GetWindow(this)).Terapija.IsEnabled = false;
            ((Doctor_Examination)Window.GetWindow(this)).Dijagnoza.IsEnabled = true;
        }

        private void Obrisi(object sender, RoutedEventArgs e)
        {
            if (medicineBox.SelectedItem != null)
            {
               Lekovi.Remove((Medicine)medicineBox.SelectedItem);
            }
        }
    }
}
