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
    /// Interaction logic for Renovations.xaml
    /// </summary>
    public partial class Renovations : Window, INotifyPropertyChanged
    {
        private RoomRenovationStorage roomRenovationStorage = new RoomRenovationStorage();
        private ObservableCollection<RoomRenovation> roomRenovations;

        public ObservableCollection<RoomRenovation> RoomRenovations
        {
            get
            {
                return roomRenovations;
            }
            set
            {
                if (value != roomRenovations)
                {
                    roomRenovations = value;
                    OnPropertyChanged("RoomRenovations");
                }
            }
        }

        public Renovations()
        {
            InitializeComponent();
            this.DataContext = this;

            RoomRenovations = roomRenovationStorage.GetAll();
        }


        private void back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addNewRenovation(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
