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
using System.Windows.Shapes;

namespace Hospital.View.Secretary
{
    /// <summary>
    /// Interaction logic for InformationBox.xaml
    /// </summary>
    public partial class InformationBox : Window, INotifyPropertyChanged
    {
        private string message;

        public string Message
        {
            get { return message; }
            set 
            { 
                message = value;
                OnPropertyChanged("Message");
            }
        }


        public InformationBox(string message)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Message = message;
        }


        private void BtnNazadClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
