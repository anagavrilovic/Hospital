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
    /// <summary>
    /// Interaction logic for IzmenaKartona.xaml
    /// </summary>
    public partial class IzmenaKartona : Window
    {
        private MedicalRecord record;

        public MedicalRecord Record
        {
            get => record;
            set
            {
                record = value;
            }
        }

        public IzmenaKartona(MedicalRecord mr)
        {
            InitializeComponent();
            this.DataContext = this;
            this.record = mr;
        }

        private void BtnPotvrdi(object sender, RoutedEventArgs e)
        {
            ImeText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            RoditeljText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            PrezimeText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Datum.GetBindingExpression(DatePicker.TextProperty).UpdateSource();
            AdresaText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            MestoText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            OpstinaText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            DrzavaText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            KnjizicaText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            LicnaText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            TelefonText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            JMBGText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            EmailText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            UsernameText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            KartonText.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            Record.Patient.Password = PasswordText.Password;

            switch (BracnoStanje.SelectedItem.ToString())
            {
                case "neoženjen - neudata": Record.Patient.MaritalStatus = MaritalStatus.neozenjen; break;
                case "oženjen - udata": Record.Patient.MaritalStatus = MaritalStatus.ozenjen; break;
                case "udovac - udovica": Record.Patient.MaritalStatus = MaritalStatus.udovac; break;
                case "razveden - razvedena": Record.Patient.MaritalStatus = MaritalStatus.razveden; break;
            }

            switch (Pol.SelectedItem.ToString())
            {
                case "muški": Record.Patient.Gender = Genders.male; break;
                case "ženski": Record.Patient.Gender = Genders.female; break;
                case "ostalo": Record.Patient.Gender = Genders.other; break;
            }

            if ((bool)DaButton.IsChecked)
            {
                Record.IsInsured = true;
            }
            else
            {
                Record.IsInsured = false;
            }

            this.Close();
        }

        private void BtnOdustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
