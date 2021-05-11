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

namespace Hospital.View.Secretary
{
    /// <summary>
    /// Interaction logic for IzbrisiPacijenta.xaml
    /// </summary>
    public partial class ConfirmBox : Window
    {
        private MedicalRecord record;

        private ObservableCollection<MedicalRecord> records = new ObservableCollection<MedicalRecord>();
        public ObservableCollection<MedicalRecord> Records { get => records; set => records = value; }

        public MedicalRecord Record
        {
            get => record;
            set
            {
                record = value;
            }
        }

        public ConfirmBox(MedicalRecord record, ObservableCollection<MedicalRecord> records)
        {
            InitializeComponent();
            this.Record = record;
            this.Records = records;
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            Records.Remove(Record);

            MedicalRecordStorage mrs = new MedicalRecordStorage();
            mrs.DoSerialization(Records);
            this.Close();
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
