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
    public partial class Terapija : Window,INotifyPropertyChanged
    {
       private  ObservableCollection<Medicine> lekovi = new ObservableCollection<Medicine>();
        public int dani;
        public event PropertyChangedEventHandler PropertyChanged;
        private Doctor_Examination parentWindow;
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
        public  string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        public Terapija(Doctor_Examination parentWindow)
        {
            this.parentWindow = parentWindow;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            InitializeComponent();
            medicineBox.ItemsSource = Lekovi;
        }

        private void DodajLek(object sender, RoutedEventArgs e)
        {
            LekListBox lb = new LekListBox(this);
            lb.ShowDialog();
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {

                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void medicineBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void Sacuvaj(object sender, RoutedEventArgs e)
        {
            foreach (Medicine lek in Lekovi)
            {
                parentWindow.Pregled.therapy.AddMedicine(lek);
            }
            parentWindow.Pregled.therapy.durationInDays = dani;
            parentWindow.Pregled.therapy.description = Text;
            this.Owner.Show();
            this.Close();
        }
    }
}
