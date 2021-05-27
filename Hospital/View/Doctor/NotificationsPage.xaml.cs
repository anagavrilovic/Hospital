using Hospital.Model;
using Hospital.Services;
using Hospital.ViewModels.Doctor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for DoktorObavestenja.xaml
    /// </summary>
    public partial class NotificationsPage : Page
    {
        public NotificationsPage(string doctorId,NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new DoctorNotificationViewModel(doctorId, navigationService); ;
            
        }

    }

}
