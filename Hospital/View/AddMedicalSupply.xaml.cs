using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for AddMedicalSupply.xaml
    /// </summary>
    public partial class AddMedicalSupply : Page
    {
        private MedicalSupply ms = new MedicalSupply();
        private ObservableCollection<MedicalSupply> supply = new ObservableCollection<MedicalSupply>();

        public MedicalSupply Ms
        {
            get { return ms;  }
            set { ms = value; }
        }

        public ObservableCollection<MedicalSupply> Supply
        {
            get { return supply;  }
            set { supply = value; }
        }

        public AddMedicalSupply(string id)
        {
            InitializeComponent();
            this.DataContext = this;
            Ms.RoomID = id;
        }

        private void accept(object o, RoutedEventArgs e)
        {
            switch (Units.SelectedIndex)
            {
                case 0: Ms.Units = UnitsType.kutije;   break;
                case 1: Ms.Units = UnitsType.trake;    break;
                case 2: Ms.Units = UnitsType.flasice;  break;
            }

            supply.Add(Ms);
            MedicalSupplyStorage msStorage = new MedicalSupplyStorage();
            bool found = false;

            foreach (MedicalSupply sup in msStorage.GetByRoomID(Ms.RoomID))
            {
                if (sup.Id.Equals(ms.Id))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                msStorage.Save(Ms);
                DynamicInventory.Supply.Add(Ms);
            }
            else
            {
                MessageBox.Show("Vec postoji stavka sa unetom oznakom!");
            }


            NavigationService.Navigate(new StaticInventory(Ms.RoomID));
        }

        private void cancel(object o, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventory(Ms.RoomID));
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventory(Ms.RoomID));
        }
    }
}
