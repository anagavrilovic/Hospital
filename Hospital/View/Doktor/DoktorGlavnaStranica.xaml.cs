using Hospital.View.Doctor;
using Hospital.View.Doktor;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for DoktorGlavnaStranica.xaml
    /// </summary>
    public partial class DoktorGlavnaStranica : Page
    {
        private string doctorId;

        public DoktorGlavnaStranica(string doctorId)
        {
            InitializeComponent();
           this.doctorId = doctorId;
        }
        private void Pregled(object sender, RoutedEventArgs e)
        {
            ((DoktorGlavniProzor)Window.GetWindow(this)).Main.Content = new KalendarTermini(doctorId);
            
          
        }

        private void Lekovi(object sender, RoutedEventArgs e)
        {
            ((DoktorGlavniProzor)Window.GetWindow(this)).Main.Content = new Lekovi();
        }

        private void Hospitalized_Click(object sender, RoutedEventArgs e)
        {
            ((DoktorGlavniProzor)Window.GetWindow(this)).Main.Navigate(new HospitalizovaniPacijenti());
        }
    }
}
