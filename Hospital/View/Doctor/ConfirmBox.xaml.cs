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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for ConfirmBox.xaml
    /// </summary>
    public partial class ConfirmBox :Window, INotifyPropertyChanged
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

        public ConfirmBox(string description)
        {
            InitializeComponent();
            this.DataContext = this;
            this.description = description;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void No_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
