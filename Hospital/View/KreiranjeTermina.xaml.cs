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
  
    public partial class KreiranjeTermina : Window
    {
        private Appointment termin;
        public Appointment Termin { get => termin; set => termin = value; }
        private string imePrezimePacijent;
        public string ImePrezimePacijent { get => imePrezimePacijent; set => imePrezimePacijent = value; }
        public KreiranjeTermina()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            pacijentBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            string[] delovi=imePrezimePacijent.Split(' ');
            trajanjeTermina.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            sala.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            switch (tipTermina.SelectedIndex)
            {
                case 0: termin.Type = AppointmentType.examination; break;
                case 1: termin.Type = AppointmentType.operation; break;
            }
            Datum.GetBindingExpression(DatePicker.TextProperty).UpdateSource();
            this.Close();
            Close();
        }
    }
}
