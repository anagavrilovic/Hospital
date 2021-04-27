using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Hospital.View
{
  
    public partial class RoomsWindow : Window
    {

        public static ObservableCollection<Room> Rooms
        {
            get;
            set;
        }

        RoomStorage rs = new RoomStorage();

        private string searchstr;

        private ICollectionView roomsCollection;

        public ICollectionView RoomsCollection
        {
            get { return roomsCollection; }
            set { roomsCollection = value; }
        }


        public RoomsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Rooms = rs.GetAll();

            RoomsCollection = CollectionViewSource.GetDefaultView(Rooms);
        }

        private void searchRooms(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                this.searchstr = textbox.Text;
                if (!string.IsNullOrEmpty(searchstr))
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(dataGridRooms.ItemsSource);
                    view.Filter = new Predicate<object>(filter);
                    this.RoomsCollection.Refresh();
                }
                else
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Rooms);
                    this.RoomsCollection.Refresh();
                }
            }
        }

        private void btnSearchMouseDown(object sender, RoutedEventArgs e)
        {
            this.RoomsCollection.Refresh();
        }


        private bool filter(object item)
        {
            if (((Room)item).Name.Contains(searchstr) || ((Room)item).Id.Contains(searchstr) || ((Room)item).Floor.ToString().Contains(searchstr))
            {
                return true;
            }
            return false;
        }

        private void addRoom(object sender, RoutedEventArgs e)
        {
            AddRoom room = new AddRoom();
            room.Owner = Application.Current.MainWindow;
            room.Show();
        }

        private void editRoom(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room) dataGridRooms.SelectedItem;

            if (selectedItem != null)
            {
                EditRoom room = new EditRoom(selectedItem);
                room.Owner = Application.Current.MainWindow;

                room.idTxt.Text =  selectedItem.Id;
                room.idTxt.IsEnabled = false;
                room.nameTxt.Text = selectedItem.Name;
                room.floorTxt.Text = selectedItem.Floor.ToString();
                room.typeCB.SelectedValue = selectedItem.Type;
                room.typeCB.Text = selectedItem.Type.ToString();
                room.statusCB.SelectedValue = selectedItem.Status;
                room.statusCB.Text = selectedItem.Status.ToString();

                room.Show();
            }    
        }

        private void deleteRoom(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room) dataGridRooms.SelectedItem;
            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da uklonite izabranu salu",
                                        "Brisanje sale",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (selectedItem != null)
                    {
                        rs.Delete(selectedItem.Id);
                    }
                }
            }
        }

        private void viewStaticInventory(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room)dataGridRooms.SelectedItem;

            if (selectedItem != null)
            {
                StaticInventory inv = new StaticInventory(selectedItem.Id);
                inv.Owner = Application.Current.MainWindow;
                inv.Show();
            }
        }

        private void viewDynamicInventory(object sender, RoutedEventArgs e)
        {  
            Room selectedItem = (Room)dataGridRooms.SelectedItem;

            if(selectedItem != null)
            {
                DynamicInventory inv = new DynamicInventory(selectedItem.Id);
                inv.Owner = Application.Current.MainWindow;
                inv.Show();
            }
            
        }

        private void renovateRoom(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room)dataGridRooms.SelectedItem;

            if(selectedItem != null)
            { 
                RenovateRoom renovateRoom = new RenovateRoom(selectedItem);
                renovateRoom.Owner = Application.Current.MainWindow;
                StringBuilder sb = new StringBuilder();
                sb.Append(selectedItem.Id);
                sb.Append("-");
                sb.Append(selectedItem.Name);
                renovateRoom.nazivTxt.Text = sb.ToString();
                renovateRoom.Show();
            }
            
        }

        private void menuButton(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
