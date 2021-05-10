using Hospital.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class EditRoom : Page
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

            RoomStatus tempStatus = (RoomStatus)Enum.Parse(typeof(RoomStatus), "SLOBODNA");
            if (statusCB.SelectedItem != null)
            {
                tempStatus = (RoomStatus)Enum.Parse(typeof(RoomStatus), statusCB.Text);
            }

            this.room = new Room { Name = tempName, Floor = tempFloor, Status = tempStatus, Type = tempType, SerializeInfo = true};

            foreach(Room r in RoomsWindow.Rooms)
            {
                if(r.Id.Equals(tempId))
                {
                    r.Name = tempName;
                    r.Floor = tempFloor;
                    r.Status = tempStatus;
                    r.Type = tempType;
                    r.SerializeInfo = true;
                }
            }

            foreach (Room r in RoomStorage.rooms)
            {
                if (r.Id.Equals(tempId))
                {
                    r.Name = tempName;
                    r.Floor = tempFloor;
                    r.Status = tempStatus;
                    r.Type = tempType;
                    r.SerializeInfo = true;
                }
            }

            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + "rooms.json"))
            {
                 JsonSerializer serializer = new JsonSerializer();
                 serializer.Serialize(file, RoomStorage.rooms);
            }

            NavigationService.Navigate(new RoomsWindow());
        }

        private void cancelEdit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }

        private void BackButtonClick (object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }
    }
}
