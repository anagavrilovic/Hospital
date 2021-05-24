using Hospital.Commands.DoctorCommands;
using Hospital.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Hospital.ViewModels
{
    class DiagnosisViewModel : ViewModel
    {
        public ICommand SaveDiagnosisCommand { get; set; }
        //private static int APPOINTMENTS_TAB = 5;
       // private static int MEDICAL_RECORD_TAB = 1;
        private string diagnosisDescription;
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

        public DiagnosisViewModel()
        {
            SaveDiagnosisCommand = new SaveDiagnosisCommand(ExecuteMethod, canExecuteMethod); 
        }

        private bool canExecuteMethod(object parameter)
        {
            return true;
        }
        private void ExecuteMethod(object parameter)
        {
            MessageBox.Show("jejjj");
           // ChangeUI();
            //((Doctor_Examination)Window.GetWindow(this)).Examintaion.diagnosis = DiagnosisDescription;
        }
        /*
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
        */

    }
}
