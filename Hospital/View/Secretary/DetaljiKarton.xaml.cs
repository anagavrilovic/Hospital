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
        private MedicalRecord record = new MedicalRecord();

        public MedicalRecord Record
        {
            get => record;
            set
            {
                record = value;
            }
        }

        public DetaljiKarton(MedicalRecord mr)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Record = mr;
        }
    }
}
