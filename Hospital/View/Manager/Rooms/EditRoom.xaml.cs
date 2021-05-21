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
        public Room EditedRoom { get; set; }
        private RoomStorage _roomStorage;

        public EditRoom(Room room)
        {
            InitializeComponent();
            this.DataContext = this;

            EditedRoom = room;
            _roomStorage = new RoomStorage();
        }

        private void acceptEdit(object sender, RoutedEventArgs e)
        {
            SaveEditedProperties();
            _roomStorage.doSerialization();

            NavigationService.Navigate(new RoomsWindow());
        }

        private void SaveEditedProperties()
        {
            idTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            nameTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            floorTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            EditedRoom.Type = (RoomType)Enum.Parse(typeof(RoomType), "SALA_ZA_PREGLEDE");

            if (typeCB.SelectedItem != null)
            {
                EditedRoom.Type = (RoomType)Enum.Parse(typeof(RoomType), typeCB.Text);
            }

            EditedRoom.Status = (RoomStatus)Enum.Parse(typeof(RoomStatus), "SLOBODNA");
            if (statusCB.SelectedItem != null)
            {
                EditedRoom.Status = (RoomStatus)Enum.Parse(typeof(RoomStatus), statusCB.Text);
            }
        }

        private void CancelEdit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }

        private void BackButtonClick (object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }
    }
}
