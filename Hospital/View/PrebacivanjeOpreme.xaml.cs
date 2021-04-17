using System;
using System.Collections.Generic;
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
    /// Interaction logic for PrebacivanjeOpreme.xaml
    /// </summary>
    public partial class PrebacivanjeOpreme : Window
    {

        private MedicalSupply supplySelected;

        public PrebacivanjeOpreme(MedicalSupply ms)
        {
            InitializeComponent();
            this.supplySelected = ms;
        }

        private void accept(object sender, RoutedEventArgs e)
        {
            if (!(string.IsNullOrEmpty(kolicinaTxt.Text) && string.IsNullOrEmpty(oznakaTxt.Text)))
            {
                MedicalSupplyStorage msStorage = new MedicalSupplyStorage();
                msStorage.UpdateSupply(this.supplySelected, oznakaTxt.Text, int.Parse(kolicinaTxt.Text));
                DynamicInventory.Supply = msStorage.GetByRoomID(this.supplySelected.RoomID);
            }

            this.Close();
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
