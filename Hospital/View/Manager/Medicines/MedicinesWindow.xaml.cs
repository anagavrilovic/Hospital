using Hospital.Model;
using Hospital.Services;
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

        private ObservableCollection<Medicine> _medicines;
        public ObservableCollection<Medicine> Medicines
        {
            get => _medicines;
            set
            {
                _medicines = value;
                OnPropertyChanged("Medicines");
            }
        }

        private string _searchCriterion;
        public ICollectionView MedicineCollection { get; set; }

        public MedicinesWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            MedicineService medicineService = new MedicineService();
            Medicines = new ObservableCollection<Medicine>(medicineService.GetAll());
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
            if (selectedMedicine == null)
                return;

            EditMedicine editMedicine = new EditMedicine(selectedMedicine);
            NavigationService.Navigate(editMedicine);
        }

        private void DeleteMedicineButtonClick(object sender, RoutedEventArgs e)
        {
            Medicine selectedMedicine = (Medicine)dataGridMedicines.SelectedItem;
            if (selectedMedicine == null)
                return;
     
            NavigationService.Navigate(new DeleteMedicine(selectedMedicine));
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
                return true;
            
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


