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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Secretary
{
    public partial class OptionBar : UserControl
    {
        public OptionBar()
        {
            InitializeComponent();
        }

        private void HelpBtn_Click(object sender, RoutedEventArgs e)
        {
            // open help page
        }

        private void NotificationBtn_Click(object sender, RoutedEventArgs e)
        {
            // notification page
        }

        private void GmailBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gmail.com");
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            // open profile settings
        }
    }
}
