using Hospital.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Hospital.View.Manager.Rooms
{
    /// <summary>
    /// Interaction logic for AddNewRoomDuringRenovation.xaml
    /// </summary>
    public partial class AddNewRoomDuringRenovation : Page
    {
        public Room NewRoom { get; set; }
        public RoomRenovation RoomRenovation { get; set; }

        private RoomStorage _roomStorage;

        public AddNewRoomDuringRenovation(RoomRenovation renovation)
        {
            InitializeComponent();
            this.DataContext = this;

            NewRoom = new Room();
            this._roomStorage = new RoomStorage();
            RoomRenovation = renovation;
        }

        private void AcceptAddingButtonClick(object sender, RoutedEventArgs e)
        {
            NewRoom.Type = (RoomType)Enum.Parse(typeof(RoomType), typeCB.Text);

            if (CheckUniquenessOfNewRoomID())
                RoomRenovation.RoomsCreatedDuringRenovation.Add(NewRoom);
            else
                MessageBox.Show("Već postoji sala sa unetom oznakom!");

            NavigationService.GoBack();
        }

        private bool CheckUniquenessOfNewRoomID()
        {
            foreach (Room room in _roomStorage.GetAll())
            {
                if (room.Id.Equals(NewRoom.Id))
                    return false;
            }
            return true;
        }

        private void CancelAddingButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

}
