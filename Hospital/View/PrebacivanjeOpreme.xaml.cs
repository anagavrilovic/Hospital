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
    public partial class PrebacivanjeOpreme : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public MedicalSupply SupplySelected { get; set; }
        private ObservableCollection<string> roomsIDs;
        public ObservableCollection<string> RoomsIDs
        {
            get
            {
                return roomsIDs;
            }

            set
            {
                roomsIDs = value;
                OnPropertyChanged("RoomsIDs");
            }
        }


        public PrebacivanjeOpreme(MedicalSupply ms)
        {
            InitializeComponent();
            RoomsIDs = new ObservableCollection<string>();
            RoomStorage roomStorage = new RoomStorage();
            foreach(Room room in roomStorage.GetAll())
            {
                RoomsIDs.Add(room.Id);
            }
            this.DataContext = this;
            SupplySelected = ms;
        }

        private void accept(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(kolicinaTxt.Text))
                return;
            
            MedicalSupplyStorage msStorage = new MedicalSupplyStorage();
            String id = typeCB.Text;
            msStorage.UpdateSupply(SupplySelected, id, int.Parse(kolicinaTxt.Text));
            DynamicInventory.Supply = msStorage.GetByRoomID(SupplySelected.RoomID);

            NavigationService.Navigate(new DynamicInventory(SupplySelected.RoomID));
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventory(SupplySelected.RoomID));
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventory(SupplySelected.RoomID));
        }
    }
}
