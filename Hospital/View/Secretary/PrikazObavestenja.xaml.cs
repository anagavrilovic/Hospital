using Hospital.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class PrikazObavestenja : Window, INotifyPropertyChanged
    {
        private Notification notification;
        public Notification Notification
        {
            get { return notification; }
            set { notification = value; OnPropertyChanged("Notification"); }
        }

        public PrikazObavestenja(Notification selectedNotification)
        {
            InitializeComponent();
            this.DataContext = this;
            Notification = selectedNotification;
        }

        private void BtnNazadClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
