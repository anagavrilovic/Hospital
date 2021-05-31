using Hospital.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hospital.View
{
    public partial class EditMedicine : Page, INotifyPropertyChanged
    {
        private Medicine _medicine;
        public Medicine Medicine
        {
            get { return _medicine; }
            set { _medicine = value; OnPropertyChanged("Medicine"); }
        }

        public EditMedicine(Medicine medicine)
        {
            InitializeComponent();
            this.DataContext = this;

            Medicine = medicine;
        }
     
        private void AcceptEditingMedicine(object sender, RoutedEventArgs e)
        {
            priceTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            quantityTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            MedicineService service = new MedicineService();
            service.EditMedicine(Medicine);

            NavigationService.Navigate(new MedicinesWindow());
        }

        private void CancelEditingMedicine(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicinesWindow());
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicinesWindow());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
