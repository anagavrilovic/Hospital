using Hospital.Model;
using Hospital.Services;
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
    /// Interaction logic for PatientRateDoctor.xaml
    /// </summary>
    public partial class PatientRateDoctor : Page
    {
        Appointment app;
        PatientComment patientComment;
        PatientCommentsService patientCommentsService = new PatientCommentsService();
        public PatientRateDoctor(Appointment app)
        {
            InitializeComponent();
            GradeButton.Focus();
            this.app = app;
         
        }

        public PatientRateDoctor()
        {
            InitializeComponent();
            GradeButton.Focus();
            this.app = null;
        }
        private void myTestKey(object sender, KeyEventArgs e)
        {
            

            if ((e.Key == Key.Add) && GradeButton.IsFocused)
            {
                int ButtonValue = Int32.Parse((String)GradeButton.Content);
                if (ButtonValue < 5) ButtonValue++;
                GradeButton.Content = ButtonValue.ToString();
                return;
            }

            if ((e.Key == Key.Subtract) && GradeButton.IsFocused)
            {
                int ButtonValue = Int32.Parse((String)GradeButton.Content);
                if (ButtonValue > 1) ButtonValue--;
                GradeButton.Content = ButtonValue.ToString();
                return;
            }

            else if ((e.Key == Key.Escape) && CommentArea.IsFocused)
            {
                Keyboard.ClearFocus();
                RateButton.Focus();
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Rate(object sender, RoutedEventArgs e)
        {
            if (app == null)
            {
                patientComment = new PatientComment(MainWindow.IDnumber, CommentArea.Text, Int32.Parse((String)GradeButton.Content), DateTime.Now);
            }
            else
            {
                patientComment = new PatientComment(MainWindow.IDnumber, app.IDAppointment, CommentArea.Text, Int32.Parse((String)GradeButton.Content), "string", DateTime.Now);
            }
            patientCommentsService.Save(patientComment);
            if (app == null)
            {
                PatientAdditionalOptions additionalOptions = new PatientAdditionalOptions();
                additionalOptions.Refresh();
                this.NavigationService.Navigate(additionalOptions);
                return;
            }
            this.NavigationService.GoBack();
        }

    }
}
