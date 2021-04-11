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

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for DoktorKarton.xaml
    /// </summary>
    public partial class DoktorKarton : Page, INotifyPropertyChanged
    {

        private MedicalRecord karton;
        private MedicalRecordStorage mStorage;
        public MedicalRecord Karton
        {
            get { return karton; }
            set
            {
                karton = value;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public DoktorKarton()
        {
            InitializeComponent();
            this.DataContext = this;
            mStorage = new MedicalRecordStorage();
           Karton= mStorage.GetOne("241521526");
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
