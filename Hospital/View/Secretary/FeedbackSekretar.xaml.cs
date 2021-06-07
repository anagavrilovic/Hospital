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

namespace Hospital.View.Secretary
{
    public partial class FeedbackSekretar : Page
    {
        public Feedback NewFeedback { get; set; }
        public FeedbackService FeedbackService { get; set; }

        public FeedbackSekretar()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeEmptyProperties();
        }

        private void InitializeEmptyProperties()
        {
            NewFeedback = new Feedback();
            FeedbackService = new FeedbackService();
        }

        private void SendFeedbackClick(object sender, RoutedEventArgs e)
        {
            NewFeedback.UserId = "5";
            FeedbackService.SaveFeedBack(NewFeedback);

            InformationBox informationBox = new InformationBox("Feedback poslat!");
            informationBox.Show();

            NavigationService.Navigate(new PocetnaStranica());
        }

        private void OdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PocetnaStranica());
        }
    }
}
