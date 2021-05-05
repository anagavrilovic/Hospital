using Hospital.Model;
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
    public partial class MedicinesWindow : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private MedicineStorage _medicineStorage;

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

        private string _searchCriterion;
        public ICollectionView MedicineCollection { get; set; }

        public MedicinesWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this._medicineStorage = new MedicineStorage();

            Medicines = _medicineStorage.GetAll();
            MedicineCollection = CollectionViewSource.GetDefaultView(Medicines);
        }

        private void AddMedicineButtonClick(object sender, RoutedEventArgs e)
        {
            AddMedicine addMedicine = new AddMedicine();
            NavigationService.Navigate(addMedicine);
        }

        private void EditMedicineButtonClick(object sender, RoutedEventArgs e)
        {
            Medicine selectedMedicine = (Medicine)dataGridMedicines.SelectedItem;
            MedicineRevision medicineRevision = new MedicineRevision();
            medicineRevision.Medicine = selectedMedicine;
           /* EditMedicine editMedicine = new EditMedicine(medicineRevision);
            editMedicine.Owner = Application.Current.MainWindow;
            editMedicine.Show();*/
        }

        private void DeleteMedicineButtonClick(object sender, RoutedEventArgs e)
        {
            Medicine selectedMedicine = (Medicine)dataGridMedicines.SelectedItem;
            if (selectedMedicine == null)
                return;

            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da izbrišete izabrani lek?",
                                                      "Brisanje leka", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this._medicineStorage.Delete(selectedMedicine.ID);
                Medicines.Remove(selectedMedicine);
            }
        }

        private void MedicinesRevision(object sender, RoutedEventArgs e)
        {
            MedicineRevisionWindow medicineRevisionWindow = new MedicineRevisionWindow();
            NavigationService.Navigate(medicineRevisionWindow);
        }

        private void SearchMedicine(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                this._searchCriterion = textbox.Text;
                if (!string.IsNullOrEmpty(_searchCriterion))
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(dataGridMedicines.ItemsSource);
                    view.Filter = new Predicate<object>(Filter);
                    this.MedicineCollection.Refresh();
                }
                else
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Medicines);
                    this.MedicineCollection.Refresh();
                }
            }
        }

        private bool Filter(object item)
        {
            if (((Medicine)item).Name.Contains(_searchCriterion) || ((Medicine)item).ID.Contains(_searchCriterion) || ((Medicine)item).Price.ToString().Contains(_searchCriterion))
            {
                return true;
            }
            return false;
        }

        private void ButtonSearchMouseDown(object sender, RoutedEventArgs e)
        {
            MedicineCollection.Refresh();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }
    }
}


