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
        private static ObservableCollection<Room> rooms;
        public static ObservableCollection<Room> Rooms
        {
            get
            {
                return rooms;
            }
            set
            {
                rooms = value;
                NotifyStaticPropertyChanged();
            }    
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
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
           
            EditRoom room = new EditRoom(selectedItem);
           // room.Owner = Application.Current.MainWindow;

            room.idTxt.Text =  selectedItem.Id;
            room.idTxt.IsEnabled = false;
            room.nameTxt.Text = selectedItem.Name;
            room.floorTxt.Text = selectedItem.Floor.ToString();
            room.typeCB.SelectedValue = selectedItem.Type;
            room.typeCB.Text = selectedItem.Type.ToString();
            room.statusCB.SelectedValue = selectedItem.Status;
            room.statusCB.Text = selectedItem.Status.ToString();

            NavigationService.Navigate(room);
        }

        private void deleteRoom(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room) dataGridRooms.SelectedItem;
            if (selectedItem == null)
                return;
            
            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da uklonite izabranu salu",
                                      "Brisanje sale",
                                       MessageBoxButton.YesNo, MessageBoxImage.Question);

           if (result == MessageBoxResult.Yes)
               rs.Delete(selectedItem.Id);
                   
        }

        private void viewStaticInventory(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room)dataGridRooms.SelectedItem;

            if (selectedItem == null)
                return;
           
            StaticInventory inv = new StaticInventory(selectedItem.Id);
            NavigationService.Navigate(inv);
        }

        private void viewDynamicInventory(object sender, RoutedEventArgs e)
        {  
            Room selectedItem = (Room)dataGridRooms.SelectedItem;

            if (selectedItem == null)
                return;

            DynamicInventory inv = new DynamicInventory(selectedItem.Id);
            NavigationService.Navigate(inv);
        }

        private void renovateRoom(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Renovations());
        }

        private void refreshView(object sender, RoutedEventArgs e)
        {
            Rooms = rs.GetAll();

        }

        private void menuButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }
    }
}
