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

namespace Hospital.View.Patient.Help
{
    /// <summary>
    /// Interaction logic for Help1.xaml
    /// </summary>
    public partial class Help1 : Page
    {
        public Help1()
        {
            InitializeComponent();
            BackButton.IsEnabled = false;
            NextButton.Focus();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
    }
}
