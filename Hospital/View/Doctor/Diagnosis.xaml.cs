using Hospital.ViewModels;
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

namespace Hospital.View.Doctor
{


    public partial class Diagnosis : Page, INotifyPropertyChanged
    {

        private string diagnosisDescription;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DiagnosisDescription
        {
            get
            {
                return diagnosisDescription;
            }
            set
            {
                if (value != null)
                {
                    diagnosisDescription = value;
                    OnPropertyChanged("DiagnosisDescription");
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
            ConfirmBox confirmBox = new ConfirmBox("Da li je potreban termin ?");
            if ((bool)confirmBox.ShowDialog())
            {
                ((AppointmentWindow)Window.GetWindow(this)).tab.SelectedIndex = 5;
                ((AppointmentWindow)Window.GetWindow(this)).AppointmentTab.IsEnabled = true;
            }
            else
            {
                ((AppointmentWindow)Window.GetWindow(this)).tab.SelectedIndex = 1;
            }
                ((AppointmentWindow)Window.GetWindow(this)).Examintaion.diagnosis = DiagnosisDescription;
            ((AppointmentWindow)Window.GetWindow(this)).DiagnosisTab.IsEnabled = false;

        }
    }
}
