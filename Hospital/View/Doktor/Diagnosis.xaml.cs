using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    

    public partial class Diagnosis : Page, INotifyPropertyChanged
    {

        private string _test1;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public string Test1
        {
            get
            {
                return _test1;
            }
            set
            {
                if (value != _test1)
                {
                    _test1 = value;
                    OnPropertyChanged("Test1");
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
  
        public Diagnosis()
        {
            InitializeComponent();           
            this.DataContext = this;
        }

        private void Sacuvaj(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li je potrebna terapija?",
                      "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ((Doctor_Examination)Window.GetWindow(this)).tab.SelectedIndex = 5;
                ((Doctor_Examination)Window.GetWindow(this)).TerminiLabela.Foreground = Brushes.White;
                ((Doctor_Examination)Window.GetWindow(this)).Termini.IsEnabled = true;
            }
            else
            {
                ((Doctor_Examination)Window.GetWindow(this)).tab.SelectedIndex = 1;
            }
                ((Doctor_Examination)Window.GetWindow(this)).Pregled.diagnosis=Test1;            
            ((Doctor_Examination)Window.GetWindow(this)).Dijagnoza.IsEnabled = false;
            ((Doctor_Examination)Window.GetWindow(this)).DiagnozaLabela.Foreground = Brushes.Black;

        }
    }
}
