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
    /// Interaction logic for PatientChooseDatesForAnAppointment.xaml
    /// </summary>
    public partial class PatientChooseDatesForAnAppointment : Window
    {
        public PatientChooseDatesForAnAppointment()
        {
            InitializeComponent();
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Prikazi(object sender, RoutedEventArgs e)
        {
            String datumVreme1 = DatumPrvi.Text.Trim() + " " + "12:00:00 AM";
            String datumVreme2 = DatumDrugi.Text.Trim() + " " + "12:00:00 AM";
            DateTime datum1;
            DateTime datum2;
            try
            {
                datum1 = DateTime.Parse(datumVreme1);
                datum2 = DateTime.Parse(datumVreme2);
            }
            catch
            {
                MessageBox.Show("Nije dobro unesen datum.\nDatum:mm/dd/yyyy");
                return;
            }

            if (datum1 > datum2)
            {
                MessageBox.Show("Datumi nisu dobro uneseni");
                return;
            }

            PatientMakeAnAppointment patientMakeAnAppointment = new PatientMakeAnAppointment(datum1,datum2);
            patientMakeAnAppointment.Show();
            this.Close();
        }
    }
}
