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
    public partial class Renovations : Page, INotifyPropertyChanged
    {
        private RoomRenovationStorage _roomRenovationStorage;
        private ObservableCollection<RoomRenovation> _scheduledRenovations;

        public ObservableCollection<RoomRenovation> RoomRenovations
        {
            get => _scheduledRenovations;
            set
            {
                _scheduledRenovations = value;
                OnPropertyChanged("RoomRenovations");
            }
        }

        public Renovations()
        {
            InitializeComponent();
            this.DataContext = this;

            _roomRenovationStorage = new RoomRenovationStorage();
            RoomRenovations = _roomRenovationStorage.GetAll();
        }


        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }

        private void ScheduleNewRenovation(object sender, RoutedEventArgs e)
        {
             RenovateRoom renovateRoom = new RenovateRoom();
             NavigationService.Navigate(renovateRoom);
        }

        private void CancelRenovation(object sender, RoutedEventArgs e)
        {
            RoomRenovation selectedItem = (RoomRenovation)listBoxRenovations.SelectedItem;

            if (selectedItem == null)
                return;

            if (selectedItem.StartDate > DateTime.Now)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da otkažete renoviranje?",
                                        "Otkazivanje renoviranja", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                        _roomRenovationStorage.Delete(selectedItem.Id);
                        RoomRenovations = _roomRenovationStorage.GetAll();
                }
            }
            else if(selectedItem.StartDate < DateTime.Now)
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
