using Hospital.View.Doktor;
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

    public partial class Anamnesis : Page, INotifyPropertyChanged
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

        public Anamnesis()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Sacuvaj(object sender, RoutedEventArgs e)
        {
            ((Doctor_Examination)Window.GetWindow(this)).Pregled.anamnesis = Test1;
            ((Doctor_Examination)Window.GetWindow(this)).AnamnezaLabela.Foreground = Brushes.Black;
            ((Doctor_Examination)Window.GetWindow(this)).Anamneza.IsEnabled = false;
            ConfirmBox confirmBox = new ConfirmBox("Da li je potrebna terapija?");
            if ((bool)confirmBox.ShowDialog())
            {                
                ((Doctor_Examination)Window.GetWindow(this)).tab.SelectedIndex = 3;
                ((Doctor_Examination)Window.GetWindow(this)).Terapija.IsEnabled = true;
                ((Doctor_Examination)Window.GetWindow(this)).TerapijaLabela.Foreground = Brushes.White;
            }
            else
            {
                ((Doctor_Examination)Window.GetWindow(this)).tab.SelectedIndex = 4;
                ((Doctor_Examination)Window.GetWindow(this)).Dijagnoza.IsEnabled = true;
                ((Doctor_Examination)Window.GetWindow(this)).DiagnozaLabela.Foreground = Brushes.White;
            }
        }
    }
}
