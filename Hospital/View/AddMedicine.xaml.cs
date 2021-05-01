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
    /// <summary>
    /// Interaction logic for AddMedicine.xaml
    /// </summary>
    public partial class AddMedicine : Window, INotifyPropertyChanged
    {
        public Medicine Medicine { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private ObservableCollection<string> doctorsNameSurname;
        public ObservableCollection<string> DoctorsNameSurname
        {
            get
            {
                return doctorsNameSurname;
            }

            set
            {
                doctorsNameSurname = value;
                OnPropertyChanged("DoctorsNameSurname");
            }
        }

        public List<string> Ingridients { get; set; }

        public AddMedicine()
        {
            InitializeComponent();
            DoctorsNameSurname = new ObservableCollection<string>();
            DoctorStorage doctorStorage = new DoctorStorage();
            foreach(Hospital.Model.Doctor doctor in doctorStorage.GetAll())
            {
                DoctorsNameSurname.Add(doctor.ToString());
            }

            this.DataContext = this;
        }

        private void SearchMedicine(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPlusIngridients(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMinusIngridients(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSearchMouseDown(object sender, RoutedEventArgs e)
        {

        }

        private void SendOnRevision(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
