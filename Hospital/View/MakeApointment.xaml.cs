using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MakeApointment.xaml
    /// </summary>
    public partial class MakeApointment : Window
    {
        private int colNum = 0;
        public ObservableCollection<Appointment> appointments
        {
            get;
            set;
        }

        public MakeApointment()
        {
            InitializeComponent();
            this.DataContext = this;
            appointments = new ObservableCollection<Appointment>();
            dataGridPregledi.Loaded += setMinWidths;
        }

        private void setMinWidths(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGridPregledi.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }
    }
}
