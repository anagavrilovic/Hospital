using Hospital.Services;
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

namespace Hospital.View.Secretary
{
    public partial class HitanPregledDetalji : Window
    {
        public Appointment Appointment { get; set; }
        public string Type { get; set; }


        public HitanPregledDetalji(Appointment newAppointment)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Appointment = newAppointment;
            LoadAppointmentType();
        }

        private void LoadAppointmentType()
        {
            if (Appointment.Type.Equals(AppointmentType.urgentExamination))
                Type = "pregled";
            else if (Appointment.Type.Equals(AppointmentType.urgentOperation))
                Type = "operacija";
        }

        private void OKbtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
