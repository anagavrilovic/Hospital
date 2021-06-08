using System;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Hospital.View.Manager
{
    public partial class TutorialPage : Page
    {
        public Uri VideoUrl { get; set; }

        public TutorialPage()
        {
            InitializeComponent();
            this.DataContext = this;

            MediaPlayer mp = new MediaPlayer();
            try {
                VideoUrl = new Uri("Media/Tutorijal.mp4", UriKind.Relative);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            tutorijal.Play();
        }

        private void PlayVideo(object sender, RoutedEventArgs e)
        {
            tutorijal.Play();
        }

        private void PauseVideo(object sender, RoutedEventArgs e)
        {
            tutorijal.Pause();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }
    }
}
