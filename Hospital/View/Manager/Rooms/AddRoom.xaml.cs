using Hospital.Model;
using Hospital.Repositories;
using Hospital.Services;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class AddRoom : Page, INotifyPropertyChanged
    {
        private Room _newRoom;
        public Room NewRoom
        {
            get { return _newRoom; }
            set { _newRoom = value; OnPropertyChanged("NewRoom"); }
        }

        public AddRoom()
        {
            InitializeComponent();
            this.DataContext = this;

            NewRoom = new Room();
        }

        private void AcceptAddingButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeNewRoom();

            RoomService roomService = new RoomService(new RoomFileRepository(), new AppointmentFileRepository());
            if (!roomService.IsNewRoomIdUnique(NewRoom.Id))
            {
                Hospital.View.Manager.MessageWindow message = new Hospital.View.Manager.MessageWindow("Vec postoji prostorija sa unetom oznakom!");
                message.Show();
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
