using Hospital.Model;
using Hospital.ViewModels.Patient;
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
    /// Interaction logic for PatientSettingsPage.xaml
    /// </summary>
    public partial class PatientSettingsPage : Page
    {
        public PatientSettingsPage(PatientSettingsPageViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            BackButton.Focus();
        }


        private void Nazad(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Space) && ComboBox.IsFocused)
            {
                Keyboard.ClearFocus();
                SaveButton.Focus();
            }
        }
    }
}
