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
    /// Interaction logic for RenovateRoom.xaml
    /// </summary>
    public partial class RenovateRoom : Window
    {
        //private Room room;
        private RoomRenovation roomRenovation;
        private RoomStorage roomStorage;

        public RoomRenovation RoomRenovation
        {
            get { return roomRenovation;  }
            set { roomRenovation = value; }
        }


        public RenovateRoom()
        {
            InitializeComponent();
            this.DataContext = this;
            // this.room = room;
            this.roomRenovation = new RoomRenovation();
            this.roomStorage = new RoomStorage();
        }

        private void accept(object sender, RoutedEventArgs e)
        { 
            // roomRenovation.Room = room;
            roomRenovation.Room = roomStorage.GetOne(roomCB.Text);
            roomRenovation.Room.SerializeInfo = false;
            roomRenovation.WareHouse = roomStorage.GetOne(magacinCB.Text);
            roomRenovation.WareHouse.SerializeInfo = false;

            if (roomRenovation.Room.Status == RoomStatus.RENOVIRA_SE)
            {
                MessageBox.Show("Izabrana sala se trenutno renovira!");
            }

            if (roomRenovation.StartDate >= roomRenovation.EndDate)
            {
                MessageBox.Show("Pogrešan izbor datuma renoviranja!");
                return;
            }

            AppointmentStorage appointmentStorage = new AppointmentStorage();

            bool isAnyAppointmentInRenovationPeriod = false;

            foreach(Appointment appointment in appointmentStorage.GetAll())
            {
                if(appointment.DateTime > roomRenovation.StartDate && appointment.DateTime < roomRenovation.EndDate + new TimeSpan(23,59,59) && appointment.Room.Id.Equals(roomRenovation.Room.Id))
                {
                    isAnyAppointmentInRenovationPeriod = true;
                    break;
                }
            }

            if(!isAnyAppointmentInRenovationPeriod)
            {
                RoomRenovationStorage storage = new RoomRenovationStorage();
                storage.Save(roomRenovation);
                
                Renovations renovations = new Renovations();
                renovations.Owner = Application.Current.MainWindow;
                renovations.Show();

                this.Close();
            }
            else 
            {
                MessageBox.Show("U izabranom periodu postoje zakazani termini pa nije moguće zakazati renoviranje!");
                return;
            }
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
            Renovations renovations = new Renovations();
            renovations.Owner = Application.Current.MainWindow;
            renovations.Show();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
