using System;
using System.Collections.Generic;
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
    /// Interaction logic for HitanPregledDetalji.xaml
    /// </summary>
    public partial class HitanPregledDetalji : Window
    {
        // Properties
        public Appointment Appointment { get; set; }
        public Model.Doctor Doctor { get; set; }
        public MedicalRecord Patient { get; set; }
        public string Type { get; set; }

        // Storage classes properties
        public Model.DoctorStorage DoctorStorage { get; set; }
        public MedicalRecordStorage MedicalRecordStorage { get; set; }


        // Methods
        public HitanPregledDetalji(Appointment newAppointment)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Appointment = newAppointment;
            LoadDoctor();
            LoadPatient();
            LoadAppointmentType();
        }

        private void LoadDoctor()
        {
            DoctorStorage = new Model.DoctorStorage();
            Doctor = DoctorStorage.GetDoctorByID(Appointment.IDDoctor);
        }

        private void LoadPatient()
        {
            MedicalRecordStorage = new MedicalRecordStorage();
            Patient = MedicalRecordStorage.GetByPatientID(Appointment.IDpatient);
        }

        private void LoadAppointmentType()
        {
            if (Appointment.Type.Equals(AppointmentType.examination))
                Type = "pregled";
            else if (Appointment.Type.Equals(AppointmentType.operation))
                Type = "operacija";
        }

        private void OKbtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
