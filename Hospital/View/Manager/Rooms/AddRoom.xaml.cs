using Hospital.Model;
using Hospital.Services;
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
        public Room NewRoom { get; set; }

        public AddRoom()
        {
            InitializeComponent();
            this.DataContext = this;

            NewRoom = new Room();
        }

        private void AcceptAddingButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeNewRoom();

            RoomService roomService = new RoomService();
            if (!roomService.IsNewRoomIdUnique(NewRoom.Id))
            {
                MessageBox.Show("Vec postoji prostorija sa unetom oznakom!");
                return;
            }
            roomService.Save(NewRoom);
         
            NavigationService.Navigate(new RoomsWindow());
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
