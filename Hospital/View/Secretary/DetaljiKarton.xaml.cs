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

namespace Hospital.View.Secretary
{
    /// <summary>
    /// Interaction logic for DetaljiKarton.xaml
    /// </summary>
    public partial class DetaljiKarton : Page
    {
        public MedicalRecord PatientsRecord { get; set; }

        public DetaljiKarton(MedicalRecord selectedPatient)
        {
            InitializeComponent();
            this.DataContext = this;
            this.PatientsRecord = selectedPatient;
        }
    }
}
