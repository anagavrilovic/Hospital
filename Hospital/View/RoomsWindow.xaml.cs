using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for RoomsWindow.xaml
    /// </summary>
    public partial class RoomsWindow : Window
    {
        public ObservableCollection<Room> Rooms
        {
            get;
            set;
        }

        public RoomsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ObservableCollection<Room> Rooms = new ObservableCollection<Room>();
            this.Rooms = Rooms;
          
            Rooms.Add(new Room { Id = 1, Name = "Sala 1", Floor = 2, IsAvaliable = false, Type = (RoomType)Enum.Parse(typeof(RoomType), "restRoom") });
           
        }

        private void addRoom(object sender, RoutedEventArgs e)
        {
            AddRoom room = new AddRoom();
            room.Owner = Application.Current.MainWindow;
            room.Show();
        }

        private void editRoom(object sender, RoutedEventArgs e)
        {

        }

        private void deleteRoom(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room) dataGridRooms.SelectedItem;

            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da uklonite izabranu salu",
                                          "Brisanje sale",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (selectedItem != null)
                {
                    this.Rooms.Remove(selectedItem);
                }
            }

          

        }
    }
}
