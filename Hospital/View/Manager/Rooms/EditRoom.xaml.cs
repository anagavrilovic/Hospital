using Hospital.Model;
using Hospital.Repositories;
using Hospital.Services;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class EditRoom : Page, INotifyPropertyChanged
    {
        private Room _editedRoom;
        public Room EditedRoom
        {
            get { return _editedRoom; }
            set { _editedRoom = value; OnPropertyChanged("EditedRoom"); }
        }

        public EditRoom(Room room)
        {
            InitializeComponent();
            this.DataContext = this;

            EditedRoom = room;
        }

        private void acceptEdit(object sender, RoutedEventArgs e)
        {
            SaveEditedProperties();

            RoomService roomService = new RoomService(new RoomFileRepository(), new AppointmentFileRepository());
            roomService.EditRoom(EditedRoom);

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
