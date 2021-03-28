using Hospital.View;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Doctor_Examination examination = new Doctor_Examination();
            examination.Show();
        }

        private void SecretaryClick(object sender, RoutedEventArgs e)
        {
            var s = new SecretaryWindow();
            s.Show();
        }

        private void DoctorClick(object sender, RoutedEventArgs e)
        {
            Doctor_Examination de = new Doctor_Examination();
            string text = "";
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\dijagnoza.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, text);
            }
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\anamneza.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, text);
            }
            de.Owner = Application.Current.MainWindow;
            de.Show();
        }

        private void ManagerClick(object sender, RoutedEventArgs e)
        {
			MenagerWindow mw = new MenagerWindow();
            mw.Owner = Application.Current.MainWindow;
            mw.Show();
        }

        private void PatientClick(object sender, RoutedEventArgs e)
        {
            PatientMain patientMain = new PatientMain();
            patientMain.Show();
        }
    }
}
