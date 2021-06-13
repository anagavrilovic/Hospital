using Hospital.Factory;
using Hospital.Model;
using Hospital.Repositories;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for PatientFeedback.xaml
    /// </summary>
    public partial class PatientFeedback : Page
    {
        private FeedbackService feedbackSevice = new FeedbackService(new FeedbackFileFactory());
        private Feedback feedback = new Feedback();
        private PatientSettingsService patientSettingsService = new PatientSettingsService(new PatientSettingsFileFactory());
        public PatientFeedback()
        {
            InitializeComponent();
            ComboBoxType.Focus();
            
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Space) && ComboBoxType.IsFocused)
            {
                Keyboard.ClearFocus();
                TextBoxContent.Focus();
            } else if ((e.Key == Key.Escape) && TextBoxContent.IsFocused)
            {
                Keyboard.ClearFocus();
                SendButton.Focus();
            }
  
        }

        public void ShowToolTip(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!patientSettingsService.GetByID(MainWindow.IDnumber).ShowTooltips) return;
            ToolTip tt = (ToolTip)(sender as Control).ToolTip;
            tt.PlacementTarget = (UIElement)sender;
            tt.Placement = PlacementMode.Right;
            tt.IsOpen = (sender as Control).IsKeyboardFocusWithin;
        }

        private void SendFeedback(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da pošaljete povratnu informaciju?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) return;
            feedback.UserId = MainWindow.IDnumber;
            feedback.Comment = TextBoxContent.Text;
            feedback.FeedbackType =(FeedbackType) ComboBoxType.SelectedItem;
            feedbackSevice.SaveFeedBack(feedback);
            this.NavigationService.GoBack();
        }
    }
}
