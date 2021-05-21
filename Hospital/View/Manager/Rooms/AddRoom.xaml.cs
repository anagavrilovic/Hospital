using Hospital.Model;
using System;
using System.Collections.Generic;
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
    public partial class AddRoom : Page
    {
        private RoomStorage _roomStorage;
        public Room NewRoom { get; set; }

        public AddRoom()
        {
            InitializeComponent();
            this.DataContext = this;

            NewRoom = new Room();
            _roomStorage = new RoomStorage();
        }

        private void AcceptAddingButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeNewRoom();

            if (!IsNewRoomIdUnique())
                return;

            _roomStorage.Save(NewRoom);
         
            NavigationService.Navigate(new RoomsWindow());
        }

        private bool IsNewRoomIdUnique()
        {
            foreach (Room room in _roomStorage.GetAll())
            {
                if (room.Id.Equals(NewRoom.Id))
                {
                    MessageBox.Show("Već postoji sala sa unetom oznakom!");
                    return false;
                }
            }
            return true;
        }

        private void InitializeNewRoom()
        {
            NewRoom.Type = (RoomType)Enum.Parse(typeof(RoomType), typeCB.Text);
            NewRoom.Status = (RoomStatus)Enum.Parse(typeof(RoomStatus), statusCB.Text);
        }

        private void CancelAddingButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }
    }
}
