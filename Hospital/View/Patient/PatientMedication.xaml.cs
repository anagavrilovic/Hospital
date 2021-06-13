using Hospital.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientMedication.xaml
    /// </summary>
    public partial class PatientMedication : Page
    {
        public PatientMedication(MedicineTherapy medicineTherapy)
        {
            InitializeComponent();
            BackButton.Focus();
            NameLabel.Content = medicineTherapy.Medicine.Name;
            DaysLabel.Content = medicineTherapy.DurationInDays;
            DailyLabel.Content = medicineTherapy.TimesPerDay;
            TextBlock.Text = medicineTherapy.Description;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
