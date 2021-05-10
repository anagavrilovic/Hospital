using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class RoomsWindow : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private static ObservableCollection<Room> _rooms;
        public static ObservableCollection<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                NotifyStaticPropertyChanged();
            }    
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        RoomStorage roomStorage = new RoomStorage();

        private string searchstr;

        public ICollectionView RoomsCollection { get; set; }


        public RoomsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Rooms = roomStorage.GetAll();

            RoomsCollection = CollectionViewSource.GetDefaultView(Rooms);
        }

        private void searchRooms(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox == null)
                return;
           
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

        private void selectFilters(object sender, RoutedEventArgs e)
        {
            FilteringInventory filteringInventory = new FilteringInventory();
            NavigationService.Navigate(filteringInventory);
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
            NavigationService.Navigate(new AddRoom());
        }

        private void editRoom(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room) dataGridRooms.SelectedItem;

            if (selectedItem == null)
                return;
           
            EditRoom editRoom = new EditRoom(selectedItem);

            editRoom.idTxt.Text =  selectedItem.Id;
            editRoom.idTxt.IsEnabled = false;
            editRoom.nameTxt.Text = selectedItem.Name;
            editRoom.floorTxt.Text = selectedItem.Floor.ToString();
            editRoom.typeCB.SelectedValue = selectedItem.Type;
            editRoom.typeCB.Text = selectedItem.Type.ToString();
            editRoom.statusCB.SelectedValue = selectedItem.Status;
            editRoom.statusCB.Text = selectedItem.Status.ToString();

            NavigationService.Navigate(editRoom);
        }

        private void deleteRoom(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room) dataGridRooms.SelectedItem;

            if (selectedItem == null)
                return;
   
            NavigationService.Navigate(new DeleteRoom (selectedItem));
        }

        private void viewStaticInventory(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room)dataGridRooms.SelectedItem;

            if (selectedItem == null)
                return;
           
            StaticInventoryView inv = new StaticInventoryView(selectedItem.Id);
            NavigationService.Navigate(inv);
        }

        private void viewDynamicInventory(object sender, RoutedEventArgs e)
        {  
            Room selectedItem = (Room)dataGridRooms.SelectedItem;

            if (selectedItem == null)
                return;

            DynamicInventoryView inv = new DynamicInventoryView(selectedItem.Id);
            NavigationService.Navigate(inv);
        }

        private void renovateRoom(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Renovations());
        }

        private void refreshView(object sender, RoutedEventArgs e)
        {
            Rooms = roomStorage.GetAll();
        }

        private void menuButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }
    }
}
