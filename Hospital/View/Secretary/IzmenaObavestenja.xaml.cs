using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for IzmenaObavestenja.xaml
    /// </summary>
    public partial class IzmenaObavestenja : Window
    {
        private Notification notification = new Notification();
        private ObservableCollection<Notification> notificationList = new ObservableCollection<Notification>();

        public ObservableCollection<Notification> NotificationList
        {
            get { return notificationList; }
            set { notificationList = value; }
        }

        public Notification Notification
        {
            get { return notification; }
            set { notification = value; }
        }

        public IzmenaObavestenja(Notification n, ObservableCollection<Notification> notifications)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Notification = n;
            this.NotificationList = notifications;
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            NaslovText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            SekretarCheck.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
            LekarCheck.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
            UpravnikCheck.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
            PacijentCheck.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
            SadrzajText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Notification.Date = DateTime.Now;

            Notification.Recipients.Clear();
            RegistratedUserStorage rus = new RegistratedUserStorage();
            ObservableCollection<RegistratedUser> regUsers = rus.GetAll();

            foreach (RegistratedUser user in regUsers)
            {
                if (Notification.Sekretar && user.Type.Equals(UserType.secretary))
                {
                    Notification.Recipients.Add(user.Username, false);
                }
                if (Notification.Lekar && user.Type.Equals(UserType.doctor))
                {
                    Notification.Recipients.Add(user.Username, false);
                }
                if (Notification.Upravnik && user.Type.Equals(UserType.manager))
                {
                    Notification.Recipients.Add(user.Username, false);
                }
                if (Notification.Pacijent && user.Type.Equals(UserType.patient))
                {
                    Notification.Recipients.Add(user.Username, false);
                }
            }

            NotificationStorage ns = new NotificationStorage();
            ns.DoSerialization(NotificationList);
            this.Close();
        }
    }
}
