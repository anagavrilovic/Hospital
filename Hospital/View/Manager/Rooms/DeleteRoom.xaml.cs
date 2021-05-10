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
    /// <summary>
    /// Interaction logic for DeletePage.xaml
    /// </summary>
    public partial class DeleteRoom : Page
    {
        public Room RoomForDeleting { get; set; }
        private RoomStorage _roomStorage;

        public DeleteRoom(Room room)
        {
            InitializeComponent();
            RoomForDeleting = room;
            _roomStorage = new RoomStorage();
        }

        private void AcceptDeleting (object sender, RoutedEventArgs e)
        {
            _roomStorage.Delete(RoomForDeleting.Id);

            NavigationService.Navigate(new RoomsWindow());
        }

        private void CancelDeleting(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }
    }
}
