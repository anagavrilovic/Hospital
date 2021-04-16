using Hospital.Model;
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
    /// Interaction logic for PrikazObavestenja.xaml
    /// </summary>
    public partial class PrikazObavestenja : Window
    {
        private Notification notification = new Notification();

        public Notification Notification
        {
            get { return notification; }
            set { notification = value; }
        }

        public PrikazObavestenja(Notification n)
        {
            InitializeComponent();
            this.DataContext = this;
            Notification = n;
        }

        private void BtnNazadClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
