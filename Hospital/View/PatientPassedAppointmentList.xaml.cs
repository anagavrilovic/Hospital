using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for PatientPassedAppointmentList.xaml
    /// </summary>
    public partial class PatientPassedAppointmentList : Window
    {
        public ObservableCollection<Appointment> Lista
        {
            get;
            set;
        }
        public PatientPassedAppointmentList()
        {
            InitializeComponent();
            this.DataContext = this;
            MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
            MedicalRecord record = medicalRecordStorage.GetByPatientID("481561361365");
            List<Examination>lista = record.Examination;
            List<Appointment> lista1 = new List<Appointment>();
            foreach(Examination examination in lista)
            {
                lista1.Add(examination.appointment);
            }
            Lista = new ObservableCollection<Appointment>(lista1);
        }
    }
}
