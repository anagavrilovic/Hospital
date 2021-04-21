using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for Lekovi.xaml
    /// </summary>
    public partial class Lekovi : Page,INotifyPropertyChanged
    {
        private MedicineStorage mStorage = new MedicineStorage();
        private Medicine lek=new Medicine();
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
        public Medicine Lek
        {
            get { return lek; }
            set
            {
                lek = value;
                OnPropertyChanged();
            }
        }
        private string sastojak;
        public string Sastojak
        {
            get { return sastojak; }
            set
            {
                sastojak = value;
                OnPropertyChanged();
            }
        }

        public Lekovi()
        {
            InitializeComponent();
            this.DataContext = this;
            sastojci.ItemsSource = Lek.Ingredient;
            listBox.ItemsSource = mStorage.GetAll();
            listaZamena.ItemsSource = Lek.Medicines;
            text="Lek: ";
        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {

                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sacuvaj.Visibility.Equals(Visibility.Hidden))
            {
                Lek = (Medicine)listBox.SelectedItem;
            }
        }

        private void Izmeni(object sender, RoutedEventArgs e)
        {
            dodajZamenu.Visibility = Visibility.Visible;
            izbaciZamenu.Visibility = Visibility.Visible;
            sacuvaj.Visibility = Visibility.Visible;
            izmeni.Visibility = Visibility.Hidden;
            listaZamena.IsEnabled = true;
            doza.IsEnabled = true;
            izbaciSastojak.Visibility = Visibility.Visible;
            dodajSastojak.Visibility = Visibility.Visible;
            dodajZamenu.IsEnabled = true;
            izbaciZamenu.IsEnabled = true;
            sacuvaj.IsEnabled = true;
            izbaciSastojak.IsEnabled = true;
            dodajSastojak.IsEnabled = true;
            sastojci.IsEnabled = true;
            txtSastojak.Visibility = Visibility.Visible;
        }

        private void dodajSastojak_Click(object sender, RoutedEventArgs e)
        {
            if (!Sastojak.Equals(""))
            {
                Ingredient i = new Ingredient();
                i.name = Sastojak;
                Lek.AddIngredient(i); 
                Sastojak = "";
                sastojci.ItemsSource = Lek.Ingredient;
                sastojci.Items.Refresh();
            }
        }

        private void izbaciSastojak_Click(object sender, RoutedEventArgs e)
        {
            if (sastojci.SelectedItem != null)
            {
                Lek.RemoveIngredient((Ingredient)sastojci.SelectedItem);
                sastojci.ItemsSource = Lek.Ingredient;
                sastojci.Items.Refresh();
            }
        }

        private void dodajZamenu_Click(object sender, RoutedEventArgs e)
        {
            if (!Lek.ID.Equals(((Medicine)listBox.SelectedItem).ID))
            {
                Lek.AddMedicine(((Medicine)listBox.SelectedItem));
                listaZamena.ItemsSource = Lek.Medicines;
                listaZamena.Items.Refresh();
            }
        }

        private void izbaciZamenu_Click(object sender, RoutedEventArgs e)
        {
            Lek.RemoveMedicine((Medicine)listBox.SelectedItem);
            listaZamena.ItemsSource = Lek.Medicines;
            listaZamena.Items.Refresh();
        }
    }
}
