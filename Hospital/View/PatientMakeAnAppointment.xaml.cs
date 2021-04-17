using Hospital.Model;
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
    /// Interaction logic for PatientMakeAnAppointment.xaml
    /// </summary>
    public partial class PatientMakeAnAppointment : Window
    {
        public ObservableCollection<Appointment> Lista
        {
            get;
            set;
        }

        

        public PatientMakeAnAppointment(DateTime vreme1, DateTime vreme2)
        {
            InitializeComponent();
            this.DataContext = this;
            Lista = new ObservableCollection<Appointment>();
            while (vreme1 <= vreme2)
            {
                for (int i = 0; i < 7; i++)
                {
                    TimeSpan ts = new TimeSpan(i + 8, 0, 0);
                    DateTime varVreme = vreme1.Date + ts;
                    Appointment appTemp = new Appointment();
                    appTemp.DateTime = varVreme;
                    appTemp.type = AppointmentType.examination;
                    PatientSettingsStorage patientSettingsStorage = new PatientSettingsStorage();
                    PatientSettings patientSettings = patientSettingsStorage.getByID(MainWindow.IDnumber);
                    DoctorStorage doctorStorage = new DoctorStorage();
                    AppointmentStorage aps = new AppointmentStorage();
                    if (!patientSettings.ChosenDoctor.Equals("Nije mi bitno"))
                    {
                        appTemp.DoctrosNameSurname = patientSettings.ChosenDoctor;
                        appTemp.IDDoctor = doctorStorage.GetIDByNameSurname(patientSettings.ChosenDoctor);
                        if (!aps.ExistByTime(varVreme, appTemp.IDDoctor)) { Lista.Add(appTemp); }
                    }
                    else
                    {
                        ObservableCollection<Doctor> doctors = doctorStorage.GetAll();
                        Boolean first = true;
                        
                        foreach(Doctor d in doctors)
                        {
                            if (aps.ExistByTime(varVreme, d.PersonalID)) continue;
                                if (first)
                            {
                                appTemp.IDDoctor = d.PersonalID;
                                appTemp.DoctrosNameSurname = d.FirstName + " " + d.LastName;
                                first = false;
                            }
                            else
                            {
                                if (aps.GetNumberOfAppointmentsForDoctor(appTemp.IDDoctor) > aps.GetNumberOfAppointmentsForDoctor(d.PersonalID))
                                {
                                    appTemp.IDDoctor = d.PersonalID;
                                    appTemp.DoctrosNameSurname = d.FirstName + " " + d.LastName;
                                    
                                }
                            }
                        }
                        if(!first) Lista.Add(appTemp);

                    }
                    
                    
                    dataGridApp.SelectedIndex = 0;

                    dataGridApp.Focus();

                }
                vreme1 = vreme1.AddDays(1);
            }
           
        }

        private void myTestKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                AppointmentStorage appointmentStorage = new AppointmentStorage();
                Appointment selectedItem = (Appointment)dataGridApp.SelectedItem;
                selectedItem.IDAppointment = appointmentStorage.GetNewID();
                MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
                selectedItem.IDpatient = medicalRecordStorage.GetByPatientID(MainWindow.IDnumber).Patient.PersonalID;
                selectedItem.patientName = medicalRecordStorage.GetByPatientID(MainWindow.IDnumber).Patient.FirstName;
                selectedItem.patientSurname = medicalRecordStorage.GetByPatientID(MainWindow.IDnumber).Patient.LastName;
                RoomStorage roomStorage = new RoomStorage();
                selectedItem.Room = roomStorage.GetOne("1");

                appointmentStorage.Save(selectedItem);
                
                this.Close();
            }
        }



    }
}
