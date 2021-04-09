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
    /// Interaction logic for EditRoom.xaml
    /// </summary>
    public partial class EditRoom : Window
    {
        private Room room;

        public EditRoom(Room room)
        {
            InitializeComponent();
            this.room = room;
        }

        private void acceptEdit(object sender, RoutedEventArgs e)
        {
           
            string tempId = idTxt.Text;
            String tempName = nameTxt.Text;
            int tempFloor = int.Parse(floorTxt.Text);
            RoomType tempType = (RoomType)Enum.Parse(typeof(RoomType), "SALA_ZA_PREGLEDE");

            if (typeCB.SelectedItem != null)
            {
                tempType = (RoomType)Enum.Parse(typeof(RoomType), typeCB.Text);
            }

            Boolean tempFree;

            if (btn2.IsChecked == true)
            {
                tempFree = false;
            }
            else
            {
                tempFree = true;
            }

            this.room = new Room { Name = tempName, Floor = tempFloor, IsAvaliable = tempFree, Type = tempType };


            foreach(Room r in RoomsWindow.Rooms)
            {
                if(r.Id.Equals(tempId))
                {
                    r.Name = tempName;
                    r.Floor = tempFloor;
                    r.IsAvaliable = tempFree;
                    r.Type = tempType;
                }
            }

           // RoomsWindow.Rooms.Add(new Room { Id = tempId, Name = tempName, Floor = tempFloor, IsAvaliable = tempFree, Type = tempType });
            this.Close();
        }

        private void cancelEdit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
