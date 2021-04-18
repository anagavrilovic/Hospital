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
    /// Interaction logic for PatientChangeAppointment.xaml
    /// </summary>
    public partial class PatientChangeAppointment : Window
    {
        private Appointment app;
        public PatientChangeAppointment(Appointment app)
        {
            InitializeComponent();
            this.app = app;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime vreme = app.DateTime;
            DateTime vreme1 = Convert.ToDateTime(TextBox1.Text);
            DateTime vreme2 = Convert.ToDateTime(TextBox2.Text);
            TimeSpan ts = new TimeSpan(0, 0, 0);
            DateTime vremeModifikovano = vreme.Date + ts;
            DateTime vremeModifikovano1 = vreme1.Date + ts;
            DateTime vremeModifikovano2 = vreme2.Date + ts;

            if (vreme1.Date > vreme2.Date || vreme1.Date < DateTime.Now)
            {
                MessageBox.Show("Nepravilno uneseni datumi");
            }
           else if ((Math.Abs(( vremeModifikovano1 - DateTime.Now).TotalDays) < 2) || (Math.Abs((vremeModifikovano - vremeModifikovano1).TotalDays) > 10) || Math.Abs((vremeModifikovano - vremeModifikovano2).TotalDays) > 10)
            {
                MessageBox.Show("Nije moguce toliko pomeriti termin");
            }
            else
            {
                PatientRescheduleAppointment patientRescheduleAppointment = new PatientRescheduleAppointment(vremeModifikovano1, vremeModifikovano2, app.IDAppointment);
                patientRescheduleAppointment.Show();
                this.Close();
            }
           

        }
    }
}
