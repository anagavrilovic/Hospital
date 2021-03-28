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

namespace Hospital.View
{
    
    public partial class Doctor_Examination : Window
    {
        private string diagnosis_text = "";

        public Doctor_Examination()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Diagnosis dijagnoza =new Diagnosis(diagnosis_text);
            dijagnoza.Owner = this;
            dijagnoza.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Anamnesis anamneza = new Anamnesis();
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
