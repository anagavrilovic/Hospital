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
        public AddRoom()
        {
            InitializeComponent();
        }

        private void AcceptAddingButtonClick(object sender, RoutedEventArgs e)
        {
            string tempId = idTxt.Text;
            String tempName = nameTxt.Text;
            int tempFloor = int.Parse(floorTxt.Text);
            RoomType tempType = (RoomType)Enum.Parse(typeof(RoomType), typeCB.Text);
            RoomStatus tempStatus = (RoomStatus)Enum.Parse(typeof(RoomStatus), statusCB.Text);

            RoomStorage roomStorage = new RoomStorage();
            roomStorage.Save(new Room { Id = tempId, Name = tempName, Floor = tempFloor, Status = tempStatus, Type = tempType});

            NavigationService.Navigate(new RoomsWindow());
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
