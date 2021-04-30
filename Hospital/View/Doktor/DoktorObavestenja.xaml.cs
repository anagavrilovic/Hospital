using Hospital.Model;
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

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for DoktorObavestenja.xaml
    /// </summary>
    public partial class DoktorObavestenja : Page,INotifyPropertyChanged
    {
        private ObservableCollection<Notification> obavestenja = new ObservableCollection<Notification>();
        private DoctorStorage doctorStorage = new DoctorStorage();
        private Hospital.Model.Doctor doctor=new Model.Doctor();
        public ObservableCollection<Notification> Obavestenja
        {
            get
            {
                return obavestenja;
            }
            set
            {
                if (value != obavestenja)
                {
                    obavestenja = value;
                    OnPropertyChanged("Obavestenja");
                }
            }
        }
        private NotificationStorage nStorage = new NotificationStorage();

        public DoktorObavestenja(string doctorId)
        {
            InitializeComponent();
            this.DataContext = this;
            doctor = doctorStorage.GetOne(doctorId);
            initProperties();
        }

        private void initProperties()
        {
            foreach(NotificationsUsers notificationsUser in nStorage.GetAllNotificationsUsers())
            {
                if (notificationsUser.Username.Equals(doctor.Username))
                {
                    Obavestenja.Add(nStorage.GetOne(notificationsUser.NotificationID));
                }
                       
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
