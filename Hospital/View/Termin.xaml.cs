using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    
    public partial class Doctor_Examination : Window,INotifyPropertyChanged
    {
        private string diagnosis_text = "";
        private string anamneza_text = "";
        private Examination pregled;

        public event PropertyChangedEventHandler PropertyChanged;

        public Examination Pregled
        {
            get { return pregled; }
            set
            {
                pregled = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public Doctor_Examination()
        {
            pregled = new Examination();
            InitializeComponent();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight*3/4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth*3/4);
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
            apointment.Owner = this;
            apointment.Show();
            this.Hide();
        }

        private void image_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Terapija(object sender, RoutedEventArgs e)
        {
            Terapija t = new Terapija(this);
            this.Hide();
            t.Owner = this;
            t.Show();
        }
    }
}
