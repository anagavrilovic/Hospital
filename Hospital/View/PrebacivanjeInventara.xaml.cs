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
    /// Interaction logic for PrebacivanjeInventara.xaml
    /// </summary>
    public partial class PrebacivanjeInventara : Window
    {
        public PrebacivanjeInventara()
        {
            InitializeComponent();
        }

        private void accept(object sender, RoutedEventArgs e)
        {

        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
