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
    /// Interaction logic for DodajObavestenje.xaml
    /// </summary>
    public partial class DodajObavestenje : Page
    {
        private Notification notification = new Notification();

        public Notification Notification
        {
            get { return notification; }
            set { notification = value; }
        }

        public DodajObavestenje()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Notification.Date = DateTime.Now;
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            NotificationStorage ns = new NotificationStorage();
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

            this.Notification.Id = this.Notification.GetHashCode().ToString();
            ns.Save(Notification);

            NavigationService.Navigate(new ObavestenjaSekretar());
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ObavestenjaSekretar());
        }

        private void PacijentiFilterTextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
