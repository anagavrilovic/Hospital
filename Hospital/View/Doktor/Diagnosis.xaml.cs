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
    

    public partial class Diagnosis : Page, INotifyPropertyChanged
    {
        private static int APPOINTMENTS_TAB = 5;
        private static int MEDICAL_RECORD_TAB = 1;
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
                if (value != diagnosisDescription)
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
            ChangeUI();
            ((Doctor_Examination)Window.GetWindow(this)).Examintaion.diagnosis = DiagnosisDescription;
        }

        private void ChangeUI()
        {
            ((Doctor_Examination)Window.GetWindow(this)).DiagnosisTab.IsEnabled = false;
            ((Doctor_Examination)Window.GetWindow(this)).DiagnosisLabel.Foreground = Brushes.Black;
            ConfirmBox confirmBox = new ConfirmBox("Da li je potreban termin ?");
            if ((bool)confirmBox.ShowDialog())
            {
                ((Doctor_Examination)Window.GetWindow(this)).tab.SelectedIndex = APPOINTMENTS_TAB;
                ((Doctor_Examination)Window.GetWindow(this)).AppointmentLabel.Foreground = Brushes.White;
                ((Doctor_Examination)Window.GetWindow(this)).AppointmentTab.IsEnabled = true;
            }
            else
            {
                ((Doctor_Examination)Window.GetWindow(this)).tab.SelectedIndex = MEDICAL_RECORD_TAB;
            }
        }
    }
}
