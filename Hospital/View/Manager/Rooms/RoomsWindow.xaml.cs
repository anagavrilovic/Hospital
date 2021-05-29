using Hospital.Model;
using Hospital.Services;
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
    public partial class RoomsWindow : Page
    {
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

        private string _searchCriterion;
        public ICollectionView RoomsCollection { get; set; }

        public RoomsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            RoomService roomService = new RoomService();
            Rooms = new ObservableCollection<Room>(roomService.GetAll());

            RoomsCollection = CollectionViewSource.GetDefaultView(Rooms);
        }

        private void SearchRooms(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox == null)
                return;
           
            this._searchCriterion = textbox.Text;
            if (!string.IsNullOrEmpty(_searchCriterion))
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(dataGridRooms.ItemsSource);
                view.Filter = new Predicate<object>(Filter);
                this.RoomsCollection.Refresh();
            }
            else
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(Rooms);
                this.RoomsCollection.Refresh();
            }    
        }

        private void FilterRoomsByInventory(object sender, RoutedEventArgs e)
        {
            FilteringInventory filteringInventory = new FilteringInventory();
            NavigationService.Navigate(filteringInventory);
        }


        private bool Filter(object item)
        {
            if (((Room)item).Name.Contains(_searchCriterion) || ((Room)item).Id.Contains(_searchCriterion) || ((Room)item).Floor.ToString().Contains(_searchCriterion))          
                return true;
            
            return false;
        }

        private void AddRoom(object sender, RoutedEventArgs e)
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

        private void DeleteRoom(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room) dataGridRooms.SelectedItem;
            if (selectedItem == null)
                return;
   
            NavigationService.Navigate(new DeleteRoom (selectedItem));
        }

        private void ViewStaticInventory(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room)dataGridRooms.SelectedItem;

            if (selectedItem == null)
                return;
           
            StaticInventoryView inv = new StaticInventoryView(selectedItem.Id);
            NavigationService.Navigate(inv);
        }

        private void ViewDynamicInventory(object sender, RoutedEventArgs e)
        {  
            Room selectedItem = (Room)dataGridRooms.SelectedItem;

            if (selectedItem == null)
                return;

            DynamicInventoryView inv = new DynamicInventoryView(selectedItem.Id);
            NavigationService.Navigate(inv);
        }

        private void RenovateRoom(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Renovations());
        }

        private void RefreshView(object sender, RoutedEventArgs e)
        {
            RoomService roomService = new RoomService();
            Rooms = new ObservableCollection<Room>(roomService.GetAll());
        }

        private void MenuButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }
    }
}
