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

namespace Hospital.View.Doktor
{
    /// <summary>
    /// Interaction logic for ErrorBox.xaml
    /// </summary>
    public partial class ErrorBox : Window,INotifyPropertyChanged
    {
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("description");
                }
            }
        }
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ErrorBox(string description)
        {
            InitializeComponent();
            this.DataContext = this;
            this.description = description;
            this.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
