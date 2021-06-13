using Hospital.Factory;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for DoktorKarton.xaml
    /// </summary>
    public partial class MedicalRecordWindow : Window, INotifyPropertyChanged
    {

        private MedicalRecord medicalRecordReview;
        private MedicalRecordService medicalRecordService;
        public MedicalRecord MedicalRecordReview
        {
            get { return medicalRecordReview; }
            set
            {
                medicalRecordReview = value;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }

        public MedicalRecordWindow(string id)
        {
            InitializeComponent();
            this.DataContext = this;
            initProperties(id);

        }

        private void initProperties(string id)
        {
            medicalRecordService = new MedicalRecordService();
            MedicalRecordReview = medicalRecordService.GetRecordByID(id);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this.Owner).Show();
            this.Close();
        }
    }
}
