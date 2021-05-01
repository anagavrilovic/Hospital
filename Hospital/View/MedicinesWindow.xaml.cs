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
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for MedicinesWindow.xaml
    /// </summary>
    public partial class MedicinesWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private MedicineStorage medicineStorage;

        private ObservableCollection<Medicine> medicines;
        public ObservableCollection<Medicine> Medicines
        {
            get => medicines;
            set
            {
                medicines = value;
                OnPropertyChanged("Medicines");
            }
        }

        private string searchstr;

        private ICollectionView medicineCollection;

        public ICollectionView MedicineCollection
        {
            get { return medicineCollection; }
            set { medicineCollection = value; }
        }

        public MedicinesWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.medicineStorage = new MedicineStorage();
            Medicines = medicineStorage.GetAll();
            MedicineCollection = CollectionViewSource.GetDefaultView(Medicines);
        }

        private void addMedicine(object sender, RoutedEventArgs e)
        {
            AddMedicine addMedicine = new AddMedicine();
            addMedicine.Owner = Application.Current.MainWindow;
            addMedicine.Show();
        }

        private void editMedicine(object sender, RoutedEventArgs e)
        {

        }

        private void deleteMedicine(object sender, RoutedEventArgs e)
        {
            Medicine selectedMedicine = (Medicine)dataGridMedicines.SelectedItem;

            if (selectedMedicine == null)
                return;

            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da izbrišete izabrani lek?",
                                                          "Brisanje leka",
                                                           MessageBoxButton.YesNo,
                                                           MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.medicineStorage.Delete(selectedMedicine.ID);
                Medicines.Remove(selectedMedicine);
            }
        }

        private void medicinesRevision(object sender, RoutedEventArgs e)
        {

        }

        private void searchMedicine(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                this.searchstr = textbox.Text;
                if (!string.IsNullOrEmpty(searchstr))
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(dataGridMedicines.ItemsSource);
                    view.Filter = new Predicate<object>(filter);
                    this.MedicineCollection.Refresh();
                }
                else
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Medicines);
                    this.MedicineCollection.Refresh();
                }
            }
        }

        private bool filter(object item)
        {
            if (((Medicine)item).Name.Contains(searchstr) || ((Medicine)item).ID.Contains(searchstr) || ((Medicine)item).Price.ToString().Contains(searchstr))
            {
                return true;
            }
            return false;
        }

        private void btnSearchMouseDown(object sender, RoutedEventArgs e)
        {
            MedicineCollection.Refresh();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}


