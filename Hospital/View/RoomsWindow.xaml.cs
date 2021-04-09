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
  
    public partial class RoomsWindow : Window
    {

        public static ObservableCollection<Room> Rooms
        {
            get;
            set;
        }

        RoomStorage rs = new RoomStorage();

        public RoomsWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            // Rooms = new ObservableCollection<Room>();
            // Rooms = RoomStorage.rooms;
            RoomStorage rs = new RoomStorage();
            Rooms = rs.GetAll();    
        }

        private void addRoom(object sender, RoutedEventArgs e)
        {
            AddRoom room = new AddRoom();
            room.Owner = Application.Current.MainWindow;
            room.Show();
        }

        private void editRoom(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room) dataGridRooms.SelectedItem;

            if (selectedItem != null)
            {
                EditRoom room = new EditRoom(selectedItem);
                room.Owner = Application.Current.MainWindow;

                room.idTxt.Text =  selectedItem.Id;
                room.idTxt.IsEnabled = false;
                room.nameTxt.Text = selectedItem.Name;
                room.floorTxt.Text = selectedItem.Floor.ToString();
                room.typeCB.SelectedValue = selectedItem.Type;
                room.typeCB.Text = selectedItem.Type.ToString();

                if(selectedItem.IsAvaliable == true)
                {
                    room.btn1.IsChecked = true;
                }
                else
                {
                    room.btn2.IsChecked = true;
                }

                room.Show();
            }    
        }

        private void deleteRoom(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room) dataGridRooms.SelectedItem;

            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da uklonite izabranu salu",
                                          "Brisanje sale",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (selectedItem != null)
                {
                    // Rooms.Remove(selectedItem);
                    rs.Delete(selectedItem.Id);
                }
            }
        }

        private void viewStaticInventory(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room)dataGridRooms.SelectedItem;

            if (selectedItem != null)
            {
                StaticInventory inv = new StaticInventory(selectedItem.Id);
                inv.Owner = Application.Current.MainWindow;
                inv.Show();
            }
        }
    }
}
