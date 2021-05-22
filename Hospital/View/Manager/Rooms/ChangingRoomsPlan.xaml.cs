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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Manager.Rooms
{
    /// <summary>
    /// Interaction logic for ChangingRoomsPlan.xaml
    /// </summary>
    public partial class ChangingRoomsPlan : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

       public Room RenovatingRoom { get; set; }
       private RoomStorage _roomStorage;

       private ObservableCollection<StringBuilder> _roomsFromSameFloor;
       public ObservableCollection<StringBuilder> RoomsFromSameFloor
        {
            get => _roomsFromSameFloor;
            set
            {
                _roomsFromSameFloor = value;
                OnPropertyChanged("RoomsFromSameFloor");
            }
        }

        public ChangingRoomsPlan(Room room)
        {
            InitializeComponent();
            RenovatingRoom = room;
            _roomStorage = new RoomStorage();
            RoomsFromSameFloor = FindRoomsFromSameFloor();
            this.DataContext = this;
        }

        private ObservableCollection<StringBuilder> FindRoomsFromSameFloor()
        {
            ObservableCollection<StringBuilder>  RoomsFromSameFloor = new ObservableCollection<StringBuilder>();
            foreach (Room r in _roomStorage.GetAll())
                if (r.Floor == RenovatingRoom.Floor && !r.Id.Equals(RenovatingRoom.Id))
                {
                    StringBuilder sb = new StringBuilder(r.Id);
                    sb.Append("-");
                    sb.Append(r.Name);
                    RoomsFromSameFloor.Add(sb);
                }

            return RoomsFromSameFloor;
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Renovations());
        }

        private void BtnAddRoomForMerge(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtnDeleteRoomForMerge(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewRoom(object sender, RoutedEventArgs e)
        {
          
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Renovations());
        }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Renovations());
        }
    }
}
