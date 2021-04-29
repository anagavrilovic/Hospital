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
             RenovateRoom renovateRoom = new RenovateRoom();
             renovateRoom.Owner = Application.Current.MainWindow;
             this.Close();
             renovateRoom.Show();
        }

        private void deleteRenovation(object sender, RoutedEventArgs e)
        {
            RoomRenovation selectedItem = (RoomRenovation)listBoxRenovations.SelectedItem;

            if (selectedItem != null && selectedItem.StartDate > DateTime.Now)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da otkažete renoviranje?",
                                        "Otkazivanje renoviranja",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (selectedItem != null)
                    {
                        roomRenovationStorage.Delete(selectedItem);
                        RoomRenovations = roomRenovationStorage.GetAll();
                    }
                }
            }
            else if(selectedItem != null && selectedItem.StartDate < DateTime.Now)
            {
                MessageBox.Show("Renoviranje je u toku. \n Naknadno otkazivanje nije moguće!");
            }
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
