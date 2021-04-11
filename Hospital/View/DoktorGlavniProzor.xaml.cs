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
    /// Interaction logic for DoktorGlavniProzor.xaml
    /// </summary>
    public partial class DoktorGlavniProzor : Window
    {
        private string IDnumber;

        public DoktorGlavniProzor(string IDnumber)
        {
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            this.IDnumber = IDnumber;
            InitializeComponent();
        }

        private void Pregled(object sender, RoutedEventArgs e)
        {
            Doctor_Examination de = new Doctor_Examination(IDnumber);
            de.Owner = Application.Current.MainWindow;
            de.Show();
            this.Hide();
        }

        private void Logo(object sender, RoutedEventArgs e)
        {

        }
    }
}
