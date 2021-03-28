using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    
    public partial class Doctor_Examination : Window
    {
        private string diagnosis_text = "";
        private string anamneza_text = "";
        public Doctor_Examination()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\dijagnoza.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                diagnosis_text = (string)serializer.Deserialize(file, typeof(string));
            }
            Diagnosis dijagnoza = new Diagnosis(diagnosis_text);
            dijagnoza.Owner = this;
            dijagnoza.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\anamneza.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                anamneza_text = (string)serializer.Deserialize(file, typeof(string));
            }
            Anamnesis anamneza = new Anamnesis(anamneza_text);
            anamneza.Owner = this;
            anamneza.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MakeApointment apointment = new MakeApointment();
            apointment.Show();
        }
    }
}
