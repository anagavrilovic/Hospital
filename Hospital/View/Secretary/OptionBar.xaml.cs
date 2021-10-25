using Hospital.ViewModels.Secretary;
using System.Windows;
using System.Windows.Controls;


namespace Hospital.View.Secretary
{
    public partial class OptionBar : UserControl
    {
        public OptionBar()
        {
            InitializeComponent();
        }

        private void HelpBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.secretaryWindow.Main.Navigate(new Help());
        }

        private void NotificationBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.secretaryWindow.Main.Navigate(new FeedbackSecretary(new FeedbackViewModel(MainWindow.secretaryWindow.Main.NavigationService)));
        }

        private void GmailBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gmail.com");
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.secretaryWindow.Main.Navigate(new MyProfile());
        }

    }
}
