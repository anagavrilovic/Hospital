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

namespace Hospital.View.Secretary
{
    /// <summary>
    /// Interaction logic for OtkazivanjeTermina.xaml
    /// </summary>
    public partial class OtkazivanjeTermina : Window
    {
        public OtkazivanjeTermina()
        {
            InitializeComponent();
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
