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

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for Help6.xaml
    /// </summary>
    public partial class Help6 : Page
    {
        public Help6()
        {
            InitializeComponent();
            NextButton.Focus();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Help7());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
    }
}
