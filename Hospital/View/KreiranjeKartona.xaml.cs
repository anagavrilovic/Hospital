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
    /// Interaction logic for KreiranjeKartona.xaml
    /// </summary>
    public partial class KreiranjeKartona : Window
    {
        private ObservableCollection<MedicalRecord> md;

        public ObservableCollection<MedicalRecord> Md
        {
            get => md;
            set
            {
                md = value;
            }
        }

        public KreiranjeKartona(ObservableCollection<MedicalRecord> md)
        {
            InitializeComponent();
            this.DataContext = this;
            this.md = md;
        }

        private void BtnPotvrdi(object sender, RoutedEventArgs e)
        {
            Patient pacijent = new Patient();
            pacijent.FirstName = ImeText.Text;
            pacijent.LastName = PrezimeText.Text;
            pacijent.Address = AdresaText.Text;
            pacijent.City = MestoText.Text;
            pacijent.Township = OpstinaText.Text;
            pacijent.Country = DrzavaText.Text;
            pacijent.CardID = LicnaText.Text;
            pacijent.PersonalID = JMBGText.Text;
            pacijent.PhoneNumber = TelefonText.Text;
            pacijent.Email = EmailText.Text;
            pacijent.Username = UsernameText.Text;
            pacijent.Password = PasswordText.Password;

            switch (BracnoStanje.SelectedItem.ToString())
            {
                case "neoženjen - neudata": pacijent.MaritalStatus = MaritalStatus.neozenjen; break;
                case "oženjen - udata": pacijent.MaritalStatus = MaritalStatus.ozenjen; break;
                case "udovac - udovica": pacijent.MaritalStatus = MaritalStatus.udovac; break;
                case "razveden - razvedena": pacijent.MaritalStatus = MaritalStatus.razveden; break;
            }

            switch (Pol.SelectedItem.ToString())
            {
                case "muški": pacijent.Gender = Genders.male; break;
                case "ženski": pacijent.Gender = Genders.female; break;
                case "ostalo": pacijent.Gender = Genders.other; break;
            }

            if (Datum.SelectedDate.Equals(null))
            {
                pacijent.DateOfBirth = new DateTime();
            }
            else
            {
                pacijent.DateOfBirth = (DateTime)Datum.SelectedDate;
            }

            MedicalRecord medicalRecord = new MedicalRecord
            {
                Patient = pacijent,
                HealthCardNumber = KnjizicaText.Text,
                ParentName = RoditeljText.Text,
                IsInsured = true,
                MedicalRecordID = 0
            };

            if (!(bool)DaButton.IsChecked)
            {
                medicalRecord.IsInsured = false;
            }

            if (!KartonText.Text.Equals(""))
            {
                medicalRecord.MedicalRecordID = Int32.Parse(KartonText.Text);
            }

            Md.Add(medicalRecord);
            this.Close();
        }

        private void BtnOdustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
