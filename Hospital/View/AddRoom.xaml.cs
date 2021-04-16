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
    /// <summary>
    /// Interaction logic for AddRoom.xaml
    /// </summary>
    public partial class AddRoom : Window
    {
        public AddRoom()
        {
            InitializeComponent();
        }

        private void acceptAdding(object sender, RoutedEventArgs e)
        {
            string tempId = idTxt.Text;
            String tempName = nameTxt.Text;
            int tempFloor = int.Parse(floorTxt.Text);
            RoomType tempType = (RoomType)Enum.Parse(typeof(RoomType), typeCB.Text);

            Boolean tempFree = true;

            if(btn2.IsChecked == true)
            {
                tempFree = false;
            }
            RoomStorage rs = new RoomStorage();
            rs.Save(new Room { Id = tempId, Name = tempName, Floor = tempFloor, IsAvaliable = tempFree, Type = tempType });
            RoomsWindow.Rooms = RoomStorage.rooms;
           // RoomsWindow.Rooms.Add(new Room { Id = tempId, Name = tempName, Floor = tempFloor, IsAvaliable = tempFree, Type = tempType });
            this.Close();
        }

        private void cancelAdding(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
