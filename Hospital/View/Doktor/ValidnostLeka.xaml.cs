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

namespace Hospital.View.Doktor
{
    /// <summary>
    /// Interaction logic for ValidnostLeka.xaml
    /// </summary>
    public partial class ValidnostLeka : Page
    {
        private Model.Doctor doctor;

        public ValidnostLeka(Model.Doctor doctor)
        {
            InitializeComponent();
            this.doctor = doctor;
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            ((DoktorGlavniProzor)Window.GetWindow(this)).Main.Navigate(new DoktorObavestenja(doctor.PersonalID));
        }
    }
}
