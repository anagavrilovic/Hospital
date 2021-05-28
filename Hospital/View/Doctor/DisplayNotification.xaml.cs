using Hospital.Model;
using Hospital.ViewModels.Doctor;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for DisplayNotification.xaml
    /// </summary>
    public partial class DisplayNotification : Window
    {

        public DisplayNotification(Notification selectedNotification)
        {
            InitializeComponent();
            DisplayNotificationViewModel viewModel = new DisplayNotificationViewModel(selectedNotification);
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }
    }
}
