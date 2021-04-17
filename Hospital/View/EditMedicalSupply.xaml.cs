using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for EditMedicalSupply.xaml
    /// </summary>
    public partial class EditMedicalSupply : Window
    {
        private MedicalSupply ms;
        private ObservableCollection<MedicalSupply> supply;

        public MedicalSupply Ms
        {
            get { return ms;  }
            set { ms = value; }
        }

        public ObservableCollection<MedicalSupply> Supply
        {
            get { return supply; }
            set { supply = value; }
        }



        public EditMedicalSupply(MedicalSupply supply)
        {
            InitializeComponent();
            this.DataContext = this;
            this.ms = supply;

            switch (ms.Units)
            {
                case UnitsType.kutije:  units.SelectedItem = kutije;  break;
                case UnitsType.trake:   units.SelectedItem = trake;   break;
                case UnitsType.flasice: units.SelectedItem = flasice; break;
            }
        }

        private void accept(object sender, RoutedEventArgs e)
        {
            oznakaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            nazivTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            cenaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            kolicinaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            switch (units.SelectedIndex)
            {
                case 0: Ms.Units = UnitsType.kutije;  break;
                case 1: Ms.Units = UnitsType.trake;   break;
                case 2: Ms.Units = UnitsType.flasice; break;
            }


            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + "medicalSupply.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, MedicalSupplyStorage.supplies);
            }
            this.Close();
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
