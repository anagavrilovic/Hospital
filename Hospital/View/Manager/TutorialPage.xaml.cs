using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Hospital.View.Manager
{
    public partial class TutorialPage : Page
    {
        public TutorialPage()
        {
            InitializeComponent();
            MediaPlayer mp = new MediaPlayer();
            try {
                mp.Open(new Uri("C:/Users/Marija/Desktop/SIMS/Git/SIMS_grupa3_tim10/Hospital/Media"));
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
