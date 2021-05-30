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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class DeleteRoom : Page
    {
        public Room RoomForDeleting { get; set; }

        public DeleteRoom(Room room)
        {
            InitializeComponent();
            RoomForDeleting = room;
        }

        private void AcceptDeleting (object sender, RoutedEventArgs e)
        {
            RoomService roomService = new RoomService();
            roomService.DeleteRoom(RoomForDeleting.Id);

            NavigationService.Navigate(new RoomsWindow());
        }

        private void CancelDeleting(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }
    }
}
